using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Client : IClient
    {
        private readonly DataContext _context;

        public Client(DataContext context)
        {
            _context = context;
        }

        public async Task<short> Post(ClientModel _client)
        {
            var duplicate = await _context.Client.AnyAsync(x => x.Name == _client.Name);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { client = "Already exists" });

            _context.Client.Add(_client);
            await _context.SaveChangesAsync();

            return _client.Id;
        }

        public async Task<ClientModel> Get(short id)
        {
            var client = await _context.Client.Include(x => x.Projects).FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
                throw new RestException(HttpStatusCode.NotFound, new { client = "Not found" });

            return client;
        }

        public async Task<List<ClientModel>> Get()
        {
            return await _context.Client.Include(x => x.Projects).ToListAsync();
        }

        public async Task<bool> Put(ClientModel _client)
        {
            var client = await _context.Client.AsNoTracking().FirstOrDefaultAsync(x => x.Id == _client.Id);
            if (client == null)
                throw new RestException(HttpStatusCode.NotFound, new { client = "Not found" });

            var duplicate = await _context.Client.AnyAsync(x => x.Name == _client.Name && x.Id != _client.Id);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { client = "Already exists" });

            var entry = _context.Entry(_client);
            entry.State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<object> Download()
        {
            var clients =
                await _context.Client.Include(x => x.Projects)
                .Select(x => new
                {
                    Cliente = x.Name,
                    No_Proyectos = x.Projects.Count,
                    Activo = x.Active ? "Sí" : "No",
                    Creado_por = x.UpdatedBy.EmployeeNumber
                })
                .ToListAsync();

            if (clients.Count == 0)
                throw new RestException(HttpStatusCode.NotFound, new { clients = "No hay registros para exportar" });

            return clients;
        }
    }
}
