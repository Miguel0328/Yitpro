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
        }
    }
}
