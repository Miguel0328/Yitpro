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

namespace Repository
{
    public class Rol : IRol
    {
        private readonly DataContext _context;

        public Rol(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRole(RoleModel role)
        {
            var duplicate = await _context.Role.AnyAsync(x => x.Name == role.Name);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { role = "Already exists" });

            _context.Role.Add(role);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<RoleModel> ReadRole(int id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null)
                throw new RestException(HttpStatusCode.NotFound, new { role = "Not found" });

            return role;
        }

        public async Task<List<RoleModel>> ReadRoles()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<bool> UpdateRole(RoleModel role)
        {
            var duplicate = await _context.Role.AnyAsync(x => x.Name == role.Name && x.Id != role.Id);
            if(duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { role = "Already exists" });

            var _role = await _context.Role.FindAsync(role.Id);
            if (_role == null)
                throw new RestException(HttpStatusCode.NotFound, new { role = "Not found" });

            _role.Name = role.Name;
            _role.Active = role.Active;
            _role.UpdatedById = role.UpdatedById;
            _role.UpdatedAt = role.UpdatedAt;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
