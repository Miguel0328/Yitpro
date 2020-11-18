using Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IRoleService
    {
        Task<bool> Post(RoleDTO _role);
        Task<bool> Put(RoleDTO _role);
        Task<List<RoleDTO>> Get();
        Task<RoleDTO> Get(short id);
        Task<List<RolePermisssionDTO>> GetPermissions(short id);
        Task<bool> PutPermissions(List<RolePermisssionDTO> permissions);
    }
}
