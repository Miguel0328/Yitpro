using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources.Errors;
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
            return await _context.User.Include(x => x.Permissions).ThenInclude(x => x.Menu).FirstOrDefaultAsync(x => x.Id == id); ;
        }
    }
}
