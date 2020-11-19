using Microsoft.EntityFrameworkCore;
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
    public class User : IUser
    {
        private readonly DataContext _context;

        public User(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> Get()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<UserModel> Get(long id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { user = "Not found" });

            return user;
        }

        public async Task<bool> Post(UserModel _user)
        {
            var duplicate = await _context.User.AnyAsync(x => x.Email == _user.Email || x.EmployeeNumber == _user.EmployeeNumber);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { user = "Already exists" });

            _context.User.Add(_user);

            return await _context.SaveChangesAsync() > 0;
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
            entry.Property(x => x.Password).IsModified = false;
            entry.Property(x => x.PasswordLastUpdate).IsModified = false;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
