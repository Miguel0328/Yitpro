using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Errors;
using Persistence.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
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

        public async Task<List<UserModel>> ReadUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<UserModel> Login(string email)
        {

            return await _context.User.FirstOrDefaultAsync(x => x.EmployeeNumber == email || x.Email == email);
        }
    }
}
