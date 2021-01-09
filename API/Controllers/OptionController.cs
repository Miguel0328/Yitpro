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
        
        [HttpGet("projects")]
        public async Task<List<OptionDTO>> GetProjects() => await _option.GetProjects();          
        
        [HttpGet("responsibles")]
        public async Task<List<OptionDTO>> GetResponsibles() => await _option.GetResponsibles();           
        
        [HttpGet("catalogs")]
        public async Task<List<OptionDTO>> GetCatalogs() => await _option.GetCatalogs();        
        
        [HttpGet("clasifications/{id}")]
        public async Task<List<OptionDTO>> GetClasifications(long id) => await _option.GetClasifications(id);

        [HttpGet("catalogs/{id}")]
        public async Task<List<OptionDTO>> GetCatalogs(long id) => await _option.GetCatalogs(id);

        [HttpGet("project/team/{id}")]
        public async Task<List<OptionDTO>> GetProjectTeam(long id) => await _option.GetProjectTeam(id);

        [HttpGet("managers")]
        public async Task<List<OptionDTO>> GetManagers() => await _option.GetManagers();
    }
}
