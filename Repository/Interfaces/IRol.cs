using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRol
    {
        Task<bool> CreateRole(RoleModel role);
        Task<bool> UpdateRole(RoleModel role);
        Task<List<RoleModel>> ReadRoles();
        Task<RoleModel> ReadRole(int id);
    }
}
