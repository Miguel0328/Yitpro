using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Option : IOption
    {
        private readonly DataContext _context;

        public Option(DataContext context)
        {
            _context = context;
        }

        public async Task<List<RoleModel>> GetRoles()
        {
            return await _context.Role.Where(x => x.Active).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<List<UserModel>> GetManagers()
        {
            return await _context.User
                .Where(x => x.Active &&
                (x.RoleId == UserRole.Admin || x.RoleId == UserRole.Manager || x.RoleId == UserRole.Leader))
                .OrderBy(x => x.FirstName + " " + x.LastName).ToListAsync();
        }

        public async Task<List<ClientModel>> GetClients()
        {
            return await _context.Client.Where(x => x.Active).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<List<CatalogModel>> GetCatalogs()
        {
            return await _context.Catalog.Where(x => x.Active && x.CatalogId == null).OrderBy(x => x.Description).ToListAsync();
        }

        public async Task<List<CatalogModel>> GetCatalogs(long id)
        {
            return await _context.Catalog.Where(x => x.Active && x.CatalogId == id).OrderBy(x => x.Description).ToListAsync();
        }
    }
}
