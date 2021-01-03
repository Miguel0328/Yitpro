using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using System;
using System.Reflection;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ClientModel> Client { get; set; }
        public DbSet<MenuModel> Menu { get; set; }
        public DbSet<PhaseModel> Phase { get; set; }
        public DbSet<RoleModel> Role { get; set; }
        public DbSet<CatalogModel> Catalog { get; set; }
        public DbSet<ProjectModel> Project { get; set; }
        public DbSet<ProjectTeamModel> ProjectTeam { get; set; }
        public DbSet<RolePermissionModel> RolePermission { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<UserPermissionModel> UserPermission { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
