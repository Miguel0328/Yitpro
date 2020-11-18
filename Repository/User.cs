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

            var user = await _context.User.FindAsync(_user.Id);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { user = "Not found" });

            user.EmployeeNumber = _user.EmployeeNumber;
            user.FirstName = _user.FirstName;
            user.LastName = _user.LastName;
            user.SecondLastName = _user.SecondLastName;
            user.Email = _user.Email;
            user.AdmissionDate = _user.AdmissionDate;
            user.RoleId = _user.RoleId;
            user.ManagerId = _user.ManagerId;
            user.Password = _user.Password;
            user.PasswordLastUpdate = _user.PasswordLastUpdate;
            user.Photo = _user.Photo;
            user.Active = _user.Active;
            user.Locked = _user.Locked;
            user.UpdatedById = _user.UpdatedById;
            user.UpdatedAt = _user.UpdatedAt;

            //_context.AddOrUpdate(_user);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
