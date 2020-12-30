using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> Get(UserFilterDTO _filter = null);
        Task<UserDTO> Get(long id);
        Task<UserDetailDTO> GetDetail(long id);
        Task<long> Post(UserDetailDTO user);
        Task<bool> Put(UserDetailDTO user);
        Task<bool> PutEnabled(UserDTO user);
        Task<List<UserPermisssionDTO>> GetPermissions(long id);
        Task<bool> PutPermissions(List<UserPermisssionDTO> permissions);
        Task<byte[]> Download();
    }
}
