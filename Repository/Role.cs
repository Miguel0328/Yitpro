using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Persistence;
using Persistence.Errors;
using Persistence.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Repository
{
    public class Role : IRole
    {
        private readonly DataContext _context;

        public Role(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Post(RoleModel _role)
        {
            var duplicate = await _context.Role.AnyAsync(x => x.Name == _role.Name);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { role = "Already exists" });

            _context.Role.Add(_role);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<RoleModel> Get(short id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null)
                throw new RestException(HttpStatusCode.NotFound, new { role = "Not found" });

            return role;
        }

        public async Task<List<RoleModel>> Get()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<bool> Put(RoleModel _role)
        {
            var duplicate = await _context.Role.AnyAsync(x => x.Name == _role.Name && x.Id != _role.Id);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { role = "Already exists" });

            var role = await _context.Role.AsNoTracking().FirstOrDefaultAsync(x => x.Id == _role.Id);
            if (role == null)
                throw new RestException(HttpStatusCode.NotFound, new { role = "Not found" });

            if (role.Protected)
                throw new RestException(HttpStatusCode.NotFound, new { role = "Protected roles cannot be modified" });

            var entry = _context.Entry(_role);
            entry.State = EntityState.Modified;
            entry.Property(x => x.Protected).IsModified = false;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<RolePermissionsModel>> GetPermissions(short id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null)
                throw new RestException(HttpStatusCode.NotFound, new { role = "Not found" });

            var permissions = await
                (from menu in _context.Menu
                 join leftPermissions in _context.RolePermissions.Where(x => x.RoleId == id) on menu.Id equals leftPermissions.MenuId into ljPermissions
                 from permission in ljPermissions.DefaultIfEmpty()
                 select new { menu, permission })
                 .Select(x => new RolePermissionsModel
                 {
                     MenuId = x.menu.Id,
                     RoleId = id,
                     Access = x.permission.Access,
                     Create = x.permission.Create,
                     Update = x.permission.Update,
                     Delete = x.permission.Delete,
                     Menu = x.menu
                 }).ToListAsync();

            return permissions;
        }

        public async Task<bool> PutPermissions(List<RolePermissionsModel> permissions)
        {
            await _context.BulkMergeAsync(permissions);
            return true;
        }
    }
}
