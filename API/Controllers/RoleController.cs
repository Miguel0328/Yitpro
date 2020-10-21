using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<bool> CreateRole(RoleDTO role) => await _role.CreateRole(role);

        [HttpGet("{id}")]
        public async Task<RoleDTO> ReadRole(int id) => await _role.ReadRole(id);

        [HttpGet]
        public async Task<List<RoleDTO>> ReadRoles() => await _role.ReadRoles();

        [HttpPut("{id}")]
        public async Task<bool> UpdateRole(int id, RoleDTO role)
        {
            role.Id = id;
            return await _role.UpdateRole(role);
        }
    }
}
