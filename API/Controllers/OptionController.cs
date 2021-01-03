using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _option;

        public OptionController(IOptionService option)
        {
            _option = option;
        }

        [HttpGet("roles")]
        public async Task<List<OptionDTO>> GetRoles() => await _option.GetRoles();              
        
        [HttpGet("clients")]
        public async Task<List<OptionDTO>> GetClients() => await _option.GetClients();           
        
        [HttpGet("catalogs")]
        public async Task<List<OptionDTO>> GetCatalogs() => await _option.GetCatalogs();        
        
        [HttpGet("managers")]
        public async Task<List<OptionDTO>> GetManagers() => await _option.GetManagers();
    }
}
