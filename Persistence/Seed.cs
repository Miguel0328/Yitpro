using Microsoft.Extensions.Configuration;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, IConfiguration config)
        {
            if (!context.Menu.Any())
            {
                var menus = new List<MenuModel>
                {
                    new MenuModel { Id = 1, Description = "Inicio", Route = "", Icon = "home", Level = 1,Order = 1000, Active = true },
                    new MenuModel { Id = 2, Description = "Catálogos", Icon = "folder open", Level = 1, Order = 2000, Active = true },
                    new MenuModel { Id = 3, ParentId = 2, Description = "Roles", Route = "role", Icon = "address card", Level = 2, Order = 2010, Active = true },
                    new MenuModel { Id = 4, ParentId = 2, Description = "Usuarios", Route = "user", Icon = "users", Level = 2, Order = 2020, Active = true },
                    new MenuModel { Id = 5, ParentId = 2, Description = "Clientes", Route = "client", Icon = "handshake", Level = 2, Order = 2030, Active = true },
                    new MenuModel { Id = 6, Description = "Proyectos", Route = "project", Icon = "paperclip", Level = 1, Order = 3000, Active = true },
                    new MenuModel { Id = 7, ParentId = 2, Description = "Catálogo general", Route = "catalog", Icon = "folder open", Level = 2, Order = 2040, Active = true },
                    new MenuModel { Id = 8, ParentId = 2, Description = "Fases", Route = "phase", Icon = "angle double up", Level = 2, Order = 2050, Active = true },
                };

                await context.Menu.AddRangeAsync(menus);
                await context.SaveChangesAsync();
            }

            if (!context.Role.Any())
            {
                var role = new RoleModel { Name = "Administrador", Protected = true, Active = true, UpdatedAt = DateTime.Now }; await context.Role.AddAsync(role);
                await context.SaveChangesAsync();

                role = new RoleModel { Name = "Gerente", Protected = true, Active = true, UpdatedAt = DateTime.Now }; await context.Role.AddAsync(role);
                await context.SaveChangesAsync();

                role = new RoleModel { Name = "Lider de Proyecto", Protected = true, Active = true, UpdatedAt = DateTime.Now }; await context.Role.AddAsync(role);
                await context.SaveChangesAsync();

                role = new RoleModel { Name = "Especialista de TI", Protected = true, Active = true, UpdatedAt = DateTime.Now }; await context.Role.AddAsync(role);
                await context.SaveChangesAsync();

                role = new RoleModel { Name = "QA", Protected = true, Active = true, UpdatedAt = DateTime.Now }; await context.Role.AddAsync(role);
                await context.SaveChangesAsync();

                role = new RoleModel { Name = "Contador", Protected = true, Active = true, UpdatedAt = DateTime.Now }; await context.Role.AddAsync(role);
                await context.SaveChangesAsync();

                var adminRolePermission = new List<RolePermissionModel>();
                foreach (var menu in context.Menu.Where(x => x.Active).ToList())
                {
                    adminRolePermission.Add(new RolePermissionModel
                    {
                        MenuId = menu.Id,
                        RoleId = 1,
                        Access = true,
                        Create = true,
                        Update = true,
                        Delete = true
                    });
                }

                await context.RolePermission.AddRangeAsync(adminRolePermission);
                await context.SaveChangesAsync();
            }

            if (!context.User.Any())
            {
                var adminUserPermission = new List<UserPermissionModel>();
                foreach (var menu in context.Menu.Where(x => x.Active).ToList())
                {
                    adminUserPermission.Add(new UserPermissionModel
                    {
                        MenuId = menu.Id,
                        UserId = 1,
                        Access = true,
                        Create = true,
                        Update = true,
                        Delete = true
                    });
                }

                var user = new UserModel
                {
                    EmployeeNumber = "Admin",
                    FirstName = "Administrador",
                    LastName = "Sistema",
                    SecondLastName = "Seeded",
                    Email = "administrador@sistema.com",
                    AdmissionDate = DateTime.Now,
                    RoleId = 1,
                    Active = true,
                    Locked = false,
                    Password = BCrypt.Net.BCrypt.HashPassword("Pa$$w0rd" + config["SecretPass"].ToString()),
                    PasswordLastUpdate = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Permissions = adminUserPermission
                }; await context.User.AddAsync(user);
                await context.SaveChangesAsync();
            }

            if (!context.Catalog.Any())
            {
                var catalogs = new List<CatalogModel>
                {
                    new CatalogModel { Description = "Proyecto - Estatus", Alias = "Proyecto - Estatus", Protected = true, Active = true, UpdatedById = 1, UpdatedAt=DateTime.Now },
                    new CatalogModel { Description = "Proyecto - Tipo", Alias = "Proyecto - Tipo", Protected = true, Active = true, UpdatedById = 1, UpdatedAt=DateTime.Now },
                    new CatalogModel { Description = "Proyecto - Metodología", Alias = "Proyecto - Metodología", Protected = true, Active = true, UpdatedById = 1, UpdatedAt=DateTime.Now },
                    new CatalogModel { Description = "Departamento", Alias = "Departamento", Protected = true, Active = true, UpdatedById = 1, UpdatedAt=DateTime.Now },
                    new CatalogModel { Description = "Proyecto - Fase", Alias = "Proyecto - Fase", Protected = true, Active = true, UpdatedById = 1, UpdatedAt=DateTime.Now },
                    new CatalogModel { Description = "Actividad - Clasificación", Alias = "Actividad - Clasificación", Protected = true, Active = true, UpdatedById = 1, UpdatedAt=DateTime.Now }
                };

                await context.Catalog.AddRangeAsync(catalogs);
                await context.SaveChangesAsync();
            }
        }
    }
}
