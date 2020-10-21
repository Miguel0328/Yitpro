using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models;
using Service.DTO;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;

        public UserController(IUserService user, IBaseService @base)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<List<UserModel>> ReadUsers() => await _user.ReadUsers();

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login) => await _user.Login(login.Email, login.Password);
    }
}
