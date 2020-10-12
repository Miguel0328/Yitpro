using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUser
    {
        Task<UserModel> Login(string nombre);
        Task<List<UserModel>> ReadUsers();
    }
}
