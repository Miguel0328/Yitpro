using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;

        public UserController(IUserService user)
        {
            _user = user;
        }

        [HttpGet("index")]
        [Authorize("Get")]
        public void Index() { return; }

        [HttpGet]
        [Authorize("Get")]
        public async Task<List<UserDTO>> Get() => await _user.Get();

        [HttpGet("{id}")]
        [Authorize("Get")]
        public async Task<UserDetailsDTO> Get(long id) => await _user.Get(id);

        [HttpPost]
        [Authorize("Post")]
        public async Task<bool> Post([FromForm] UserDetailsDTO user) => await _user.Post(user);

        [HttpPut]
        [Authorize("Put")]
        public async Task<bool> Put([FromForm] UserDetailsDTO user) => await _user.Put(user);

        [HttpPut("active")]
        [Authorize("Put")]
        public async Task<bool> PutEnabled(UserDTO user) => await _user.PutEnabled(user);

        [HttpGet("permissions/{id}")]
        [Authorize("Get")]
        public async Task<List<UserPermisssionDTO>> GetPermissions(long id) => await _user.GetPermissions(id);

        [HttpPut("permissions")]
        [Authorize("Put")]
        public async Task<bool> PutPermissions(List<UserPermisssionDTO> permissions) => await _user.PutPermissions(permissions);
    }
}
