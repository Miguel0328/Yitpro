using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface IOption
    {
        Task<List<RoleModel>> GetRoles();
        Task<List<UserModel>> GetManagers();
        Task<List<ClientModel>> GetClients();
        Task<List<CatalogModel>> GetCatalogs();

    }
}
