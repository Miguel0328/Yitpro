﻿using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
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
            return await _context.User.Where(x => x.Active && new[] { 1, 2, 3 }.Contains(x.RoleId)).OrderBy(x => x.FirstName + " " + x.LastName).ToListAsync();
        }

        public async Task<List<ClientModel>> GetClients()
        {
            return await _context.Client.Where(x => x.Active).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<List<CatalogModel>> GetCatalogs()
        {
            return await _context.Catalog.Where(x => x.Active && x.CatalogId == null).OrderBy(x => x.Description).ToListAsync();
        }
    }
}
