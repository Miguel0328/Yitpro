using Persistence.Models;
using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUser
    {
        Task<long> Post(UserModel _user);
        Task<bool> Put(UserModel _user);
        Task<bool> PutEnabled(UserModel _user);
        Task<List<UserModel>> Get(UserFilterDTO _filter);
        Task<UserModel> GetDetail(long id);
        Task<List<UserPermissionModel>> GetPermissions(long id);
        Task<bool> PutPermissions(List<UserPermissionModel> permissions);
        Task<object> Download();
    }
}
