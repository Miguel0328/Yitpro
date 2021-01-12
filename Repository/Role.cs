using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Persistence;
using Resources.Errors;
using Persistence.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using EFCore.BulkExtensions;
using Resources.Constants;

namespace Repository
{
    public class Role : IRole
    {
        private readonly DataContext _context;

        public Role(DataContext context)
        {
            _context = context;
        }

        public async Task<short> Post(RoleModel _role)
        {
            var duplicate = await _context.Role.FirstOrDefaultAsync(x => x.Name == _role.Name) != null;
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { role = Messages.Duplicated });

            _context.Role.Add(_role);
            await _context.SaveChangesAsync();

            return _role.Id;
        }

        public async Task<RoleModel> Get(short id)
        {
            return await _context.Role.FindAsync(id);
        }

        public async Task<List<RoleModel>> Get()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<bool> Put(RoleModel _role)
        {
            var role = await _context.Role.FindAsync(_role.Id);
            if (role == null)
                throw new RestException(HttpStatusCode.NotFound, new { role = Messages.NotFound });

            if (role.Protected)
                throw new RestException(HttpStatusCode.NotFound, new { role = Messages.ProtectedChanged });

            var duplicate = await _context.Role.FirstOrDefaultAsync(x => x.Name == _role.Name && x.Id != _role.Id) != null;
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { role = Messages.Duplicated });

            var entry = _context.Entry(_role);
            entry.State = EntityState.Modified;
            entry.Property(x => x.Protected).IsModified = false;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<RolePermissionModel>> GetPermissions(short id)
        {
            var permissions = await
                (from menu in _context.Menu
                 join leftPermission in _context.RolePermission.Where(x => x.RoleId == id) on menu.Id equals leftPermission.MenuId into ljPermissions
                 from permission in ljPermissions.DefaultIfEmpty()
                 orderby menu.Order
                 select new { menu, permission = permission ?? new RolePermissionModel() })
                 .Select(x => new RolePermissionModel
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

        public async Task<bool> PutPermissions(List<RolePermissionModel> permissions)
        {
            await _context.BulkInsertOrUpdateAsync(permissions);
            return true;
        }

        public async Task<object> Download()
        {
            var roles =
                await _context.Role
                .Select(x => new
                {
                    Rol = x.Name,
                    Protegido = x.Protected ? "Sí" : "No",
                    Activo = x.Active ? "Sí" : "No",
                    Creado_por = x.UpdatedBy.EmployeeNumber
                })
                .ToListAsync();

            if (roles.Count == 0)
                throw new RestException(HttpStatusCode.NotFound, new { Roles = Messages.NothingToExport });

            return roles;
        }
    }
}
