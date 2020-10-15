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
            if (!context.User.Any())
            {
                var userAdmin = new UserModel
                {
                    FirstName = "Administrador",
                    LastName = "Sistema",
                    EmployeeNumber = "ADM_SIS",
                    Email = "administrador@sistema.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Pa$$w0rd" + config["SecretPass"].ToString())
                };

                await context.User.AddAsync(userAdmin);
                await context.SaveChangesAsync();
            }

            if (!context.Menu.Any())
            {
                var menuDefault = new List<MenuModel>
                {
                    new MenuModel{
                        Id = 1,
                        IdParent = 1,
                        Description = "Dashboard",
                        Active = true,
                        Order = 1,
                        Level = 1
                    },
                    new MenuModel{
                        Id = 2,
                        Description = "Kanban",
                        Active = true,
                        Order = 1,
                        Level = 1
                    },
                    new MenuModel{
                        Id = 3,
                        Description = "Panel de Actividades",
                        Active = true,
                        Order = 1,
                        Level = 1
                    },
                    new MenuModel{
                        Id = 4,
                        IdParent = 2,
                        Description = "Reportes 1",
                        Active = true,
                        Order = 1,
                        Level = 1
                    },
                    new MenuModel{
                        Id = 5,
                        IdParent = 3,
                        Description = "Reporte 2",
                        Active = true,
                        Order = 1,
                        Level = 1
                    },
                    new MenuModel{
                        Id = 6,
                        IdParent = 3,
                        Description = "Reporte 3",
                        Active = true,
                        Order = 1,
                        Level = 1
                    },
                };
                await context.Menu.AddRangeAsync(menuDefault);
                await context.SaveChangesAsync();
            }
        }
    }
}
