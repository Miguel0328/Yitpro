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
using System.Linq;

namespace Repository
{
    public class Profile : IProfile
    {
        private readonly DataContext _context;

        public Profile(DataContext context)
        {
            _context = context;
        }

        public async Task<UserModel> Login(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.EmployeeNumber == email || x.Email == email);
        }

        public async Task<UserModel> CurrentUser(long id)
        {
            var user = await _context.User.Include(x => x.Role.Permissions).ThenInclude(x => x.Menu).FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }
    }
}
