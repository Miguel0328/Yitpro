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
        Task<List<ProjectModel>> GetProjects(long id);
        Task<List<UserModel>> GetResponsibles();
        Task<List<CatalogModel>> GetCatalogs();
        Task<List<CatalogModel>> GetCatalogs(long id);
        Task<List<PhaseModel>> GetClasifications(long id);
        Task<List<UserModel>> GetProjectTeam(long id);

    }
}
