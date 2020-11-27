using Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> Get();
        Task<UserDetailsDTO> Get(long id);
        Task<bool> Post(UserDetailsDTO user);
        Task<bool> Put(UserDetailsDTO user);
        Task<bool> PutEnabled(UserDTO user);
        Task<List<UserPermisssionDTO>> GetPermissions(long id);
        Task<bool> PutPermissions(List<UserPermisssionDTO> permissions);
    }
}
