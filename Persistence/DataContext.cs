﻿using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using System;
using System.Reflection;
using static Persistence.EntityConfiguration.ViewConfigurations;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<UserModel> User { get; set; }
        public DbSet<ViewModel> View { get; set; }
        public DbSet<MenuModel> Menu { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
