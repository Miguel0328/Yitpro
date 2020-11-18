using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUser
    {
        Task<bool> Post(UserModel _user);
        Task<bool> Put(UserModel _user);
        Task<List<UserModel>> Get();
        Task<UserModel> Get(long id);
        //Task<List<RolePermissionsModel>> GetPermissions(short id);
        //Task<bool> PutPermissions(List<RolePermissionsModel> permissions);
    }
}
