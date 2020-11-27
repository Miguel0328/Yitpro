using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Errors;
using Persistence.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class User : IUser
    {
        private readonly DataContext _context;

        public User(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> Get()
        {
            return await _context.User.Include(x => x.Role).ToListAsync();
        }

        public async Task<UserModel> Get(long id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { user = "Not found" });

            return user;
        }

        public async Task<bool> Post(UserModel user)
        {
            var duplicate = await _context.User.AnyAsync(x => x.Email == user.Email || x.EmployeeNumber == user.EmployeeNumber);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { user = "Already exists" });

            _context.User.Add(user);

            await _context.SaveChangesAsync();
            await SetPermissions(user);

            return true;
        }

        public async Task<bool> Put(UserModel _user)
        {
            var duplicate = await _context.User
                .AnyAsync(x => (x.Email == _user.Email || x.EmployeeNumber == _user.EmployeeNumber) && x.Id != _user.Id);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { user = "Already exists" });

            var user = await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Id == _user.Id);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { user = "Not found" });

            var entry = _context.Entry(_user);
            entry.State = EntityState.Modified;
            if (_user.Photo == null)
                entry.Property(x => x.Photo).IsModified = false;
            entry.Property(x => x.Password).IsModified = false;
            entry.Property(x => x.PasswordLastUpdate).IsModified = false;

            if (user.RoleId != _user.RoleId)
                await SetPermissions(_user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> PutEnabled(UserModel _user)
        {
            var user = await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Id == _user.Id);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { user = "Not found" });

            var entry = _context.Attach(_user);
            entry.Property(x => x.Active).IsModified = true;
            entry.Property(x => x.UpdatedById).IsModified = true;
            entry.Property(x => x.UpdatedAt).IsModified = true;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<UserPermissionsModel>> GetPermissions(long id)
        {
            var User = await _context.User.FindAsync(id);
            if (User == null)
                throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

            var permissions = await
                (from menu in _context.Menu
                 join leftPermissions in _context.UserPermissions.Where(x => x.UserId == id) on menu.Id equals leftPermissions.MenuId into ljPermissions
                 from permission in ljPermissions.DefaultIfEmpty()
                 select new { menu, permission })
                 .Select(x => new UserPermissionsModel
                 {
                     MenuId = x.menu.Id,
                     UserId = id,
                     Access = x.permission.Access,
                     Create = x.permission.Create,
                     Update = x.permission.Update,
                     Delete = x.permission.Delete,
                     Menu = x.menu
                 }).ToListAsync();

            return permissions;
        }

        public async Task<bool> PutPermissions(List<UserPermissionsModel> permissions)
        {
            await _context.BulkMergeAsync(permissions);
            return true;
        }

        public async Task SetPermissions(UserModel user)
        {
            var rolePermissions = await _context.RolePermissions.Where(x => x.RoleId == user.RoleId).ToListAsync();
            var userPermissions = await _context.UserPermissions.Where(x => x.UserId == user.Id).ToListAsync();
            var permissions =
                rolePermissions
                .Select(x => new UserPermissionsModel
                {
                    MenuId = x.MenuId,
                    UserId = user.Id,
                    Access = x.Access,
                    Create = x.Create,
                    Update = x.Update,
                    Delete = x.Delete,
                    UpdatedById = (long)user.UpdatedById,
                    UpdatedAt = DateTime.Now
                }).ToList();

            await _context.BulkDeleteAsync(userPermissions);
            await _context.BulkInsertAsync(permissions);
        }
    }
}
