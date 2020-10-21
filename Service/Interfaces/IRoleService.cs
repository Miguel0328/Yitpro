using Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IRoleService
    {
        Task<bool> CreateRole(RoleDTO role);
        Task<bool> UpdateRole(RoleDTO role);
        Task<List<RoleDTO>> ReadRoles();
        Task<RoleDTO> ReadRole(int id);
    }
}
