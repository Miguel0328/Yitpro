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
            if (!context.Role.Any())
            {
                var adminRole = new RoleModel
                {
                    Name = "Admin",
                    Protected = true,
                    Active = true,
                    CreatedAt = DateTime.Now
                };

                await context.Role.AddAsync(adminRole);
            }

            if (!context.User.Any())
            {
                var adminUser = new UserModel
                {
                    EmployeeNumber = "Admin",
                    FirstName = "Administrador",
                    LastName = "Sistema",
                    Email = "administrador@sistema.com",
                    AdmissionDate = DateTime.Now,
                    RoleId = 1,
                    Active=true,
                    Locked = false,
                    Password = BCrypt.Net.BCrypt.HashPassword("Pa$$w0rd" + config["SecretPass"].ToString()),
                    PasswordLastUpdate = DateTime.Now,
                    CreatedAt = DateTime.Now
                };

                await context.User.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
