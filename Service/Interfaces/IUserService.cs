using Persistence.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Login(string name, string password);
        Task<List<UserModel>> ReadUsers();
    }
}
