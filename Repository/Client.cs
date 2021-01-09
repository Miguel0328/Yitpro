using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Constants;
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
            var duplicate = await _context.Client.FirstOrDefaultAsync(x => x.Name == _client.Name) != null;
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { client = Messages.Duplicated });

            _context.Client.Add(_client);
            await _context.SaveChangesAsync();

            return _client.Id;
        }

        public async Task<ClientModel> Get(short id)
        {
            return await _context.Client.Include(x => x.Projects).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ClientModel>> Get()
        {
            return await _context.Client.Include(x => x.Projects).ToListAsync();
        }

        public async Task<bool> Put(ClientModel _client)
        {
            var client = await _context.Client.FindAsync( _client.Id);
            if (client == null)
                throw new RestException(HttpStatusCode.NotFound, new { client = Messages.NotFound });

            var duplicate = await _context.Client.FirstOrDefaultAsync(x => x.Name == _client.Name && x.Id != _client.Id) != null;
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { client = Messages.Duplicated });

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
                throw new RestException(HttpStatusCode.NotFound, new { clients = Messages.NothingToExport });

            return clients;
        }
    }
}
