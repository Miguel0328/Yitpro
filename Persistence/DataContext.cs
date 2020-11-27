using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using System;
using System.Reflection;
using Z.EntityFramework.Extensions;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            EntityFrameworkManager.ContextFactory = context => new DataContext(options);
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<UserPermissionsModel> UserPermissions { get; set; }
        public DbSet<MenuModel> Menu { get; set; }
        public DbSet<RoleModel> Role { get; set; }
        public DbSet<RolePermissionsModel> RolePermissions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
