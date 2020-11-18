using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _role;

        public RoleController(IRoleService role)
        {
            _role = role;
        }

        [HttpGet("index")]
        [Authorize("View")]
        public void Index() { return; }

        [HttpPost]
        [Authorize("Create")]
        public async Task<bool> Post(RoleDTO role) => await _role.Post(role);

        [HttpGet("{id}")]
        public async Task<RoleDTO> Get(short id) => await _role.Get(id);

        [HttpGet]
        [Authorize("View")]
        public async Task<List<RoleDTO>> Get() => await _role.Get();

        [HttpPut]
        public async Task<bool> Put(RoleDTO role) => await _role.Put(role);

        [HttpGet("permissions/{id}")]
        public async Task<List<RolePermisssionDTO>> GetPermissions(short id) => await _role.GetPermissions(id);

        [HttpPut("permissions")]
        public async Task<bool> PutPermissions(List<RolePermisssionDTO> permissions) => await _role.PutPermissions(permissions);
    }
}
