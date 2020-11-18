using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRole
    {
        Task<bool> Post(RoleModel _role);
        Task<bool> Put(RoleModel _role);
        Task<List<RoleModel>> Get();
        Task<RoleModel> Get(short id);
        Task<List<RolePermissionsModel>> GetPermissions(short id);
        Task<bool> PutPermissions(List<RolePermissionsModel> permissions);
    }
}
