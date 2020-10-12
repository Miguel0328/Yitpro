using Microsoft.Extensions.Configuration;
using Persistence.Errors;
using Persistence.Models;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUser _user;
        private readonly IBaseService _baseService;
        private readonly string _secretPassword;

        public UserService(IConfiguration config, IUser user, IBaseService baseService)
        {
            _user = user;
            _baseService = baseService;
            _secretPassword = config["SecretPass"].ToString();
        }

        public async Task<List<UserModel>> ReadUsers()
        {
            return await _user.ReadUsers();
        }

        public async Task<UserDTO> Login(string email, string password)
        {
            var user = await _user.Login(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password + _secretPassword, user.Password))
                throw new RestException(HttpStatusCode.Unauthorized);

            return new UserDTO
            {
                Name = user.FirstName,
                Token = _baseService.CreateToken(user.EmployeeNumber)
            };
        }
    }
}
