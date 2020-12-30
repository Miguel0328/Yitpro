using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resources.Constants;
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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _Project;

        public ProjectController(IProjectService Project)
        {
            _Project = Project;
        }

        [HttpGet("index")]
        [Authorize("Get")]
        public void Index() { return; }

        [HttpGet]
        [Authorize("Get")]
        public async Task<List<ProjectDTO>> Get() => await _Project.Get();

        [HttpPost("filter")]
        [Authorize("Get")]
        public async Task<List<ProjectDTO>> Get(ProjectFilterDTO filter) => await _Project.Get(filter);

        [HttpGet("{id}")]
        [Authorize("Get")]
        public async Task<ProjectDTO> Get(long id) => await _Project.Get(id);          
        
        [HttpGet("team/{id}")]
        [Authorize("Get")]
        public async Task<List<ProjectTeamDTO>> GetTeam(long id) => await _Project.GetTeam(id);        
        
        [HttpGet("get-id/{code}")]
        [Authorize("Get")]
        public async Task<long> Get(string code) => await _Project.GetId(code);

        [HttpGet("detail/{id}")]
        [Authorize("Get")]
        public async Task<ProjectDetailDTO> GetDetail(long id) => await _Project.GetDetail(id);

        [HttpPost]
        [Authorize("Post")]
        public async Task<long> Post(ProjectDetailDTO Project) => await _Project.Post(Project);

        [HttpPut]
        [Authorize("Put")]
        public async Task<bool> Put(ProjectDetailDTO Project) => await _Project.Put(Project);

        [HttpPut("active")]
        [Authorize("Put")]
        public async Task<bool> PutEnabled(ProjectDTO Project) => await _Project.PutEnabled(Project);


        [HttpGet("download")]
        public async Task<IActionResult> Download() => File(await _Project.Download(), MimeType.XLSX);
    }
}
