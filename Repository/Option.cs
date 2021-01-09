using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Constants;
using Resources.Extension;
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

        public async Task<List<PhaseModel>> GetClasifications(long id)
        {
            return await _context.Phase.Include(x => x.Clasification)
                .Where(x => x.Active && x.PhaseId == id)
                .OrderBy(x => x.Clasification.Description)
                .ToListAsync();
        }

        public async Task<List<UserModel>> GetProjectTeam(long id)
        {
            return await _context.ProjectTeam.Include(x => x.User)
                .Where(x => x.ProjectId == id && x.Active)
                .Select(x => x.User)
                .OrderBy(x => x.FirstName + " " + x.LastName)
                .ToListAsync();
        }

        public async Task<List<ProjectModel>> GetProjects(long id)
        {
            var user = await _context.User.FindAsync(id);
            return await user.GetProjects(_context).Where(x => x.Active).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<List<UserModel>> GetResponsibles()
        {
            return await _context.User
                .Where(x => x.Active &&
                (x.RoleId == UserRole.QA || x.RoleId == UserRole.Manager || x.RoleId == UserRole.Leader))
                .OrderBy(x => x.FirstName + " " + x.LastName).ToListAsync();
        }
    }
}
