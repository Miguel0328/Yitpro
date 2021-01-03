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

        [HttpGet("index/detail/{id}")]
        [Authorize("Get")]
        [Authorize("Project")]
        public void IndexDetail() { return; }

        [HttpGet]
        [Authorize("Get")]
        public async Task<List<ProjectDTO>> Get() => await _Project.Get();

        [HttpPost("filter")]
        [Authorize("Get")]
        public async Task<List<ProjectDTO>> Get(ProjectFilterDTO filter) => await _Project.Get(filter);

        [HttpGet("{id}")]
        [Authorize("Get")]
        [Authorize("Project")]
        public async Task<ProjectDTO> Get(long id) => await _Project.Get(id);

        [HttpGet("team/{id}")]
        [Authorize("Get")]
        [Authorize("Project")]
        public async Task<List<ProjectTeamDTO>> GetTeam(long id) => await _Project.GetTeam(id);

        [HttpGet("team/remaining/{id}")]
        [Authorize("Get")]
        [Authorize("Project")]
        public async Task<List<UserDTO>> GetRemainingTeam(long id) => await _Project.GetRemainingTeam(id);

        [HttpGet("get-id/{code}")]
        [Authorize("Get")]
        public async Task<long> Get(string code) => await _Project.GetId(code);

        [HttpGet("detail/{id}")]
        [Authorize("Get")]
        [Authorize("Project")]
        public async Task<ProjectDetailDTO> GetDetail(long id) => await _Project.GetDetail(id);

        [HttpPost]
        [Authorize("Post")]
        public async Task<long> Post(ProjectDetailDTO project) => await _Project.Post(project);

        [HttpPost("team/{id}")]
        [Authorize("Post")]
        [Authorize("Project")]
        public async Task<bool> PostTeam(SelectedDTO newTeam) => await _Project.PostTeam(newTeam);

        [HttpPut("{id}")]
        [Authorize("Put")]
        [Authorize("Project")]
        public async Task<bool> Put(ProjectDetailDTO project) => await _Project.Put(project);

        [HttpPut("active/{id}")]
        [Authorize("Put")]
        [Authorize("Project")]
        public async Task<bool> PutEnabled(ProjectDTO project) => await _Project.PutEnabled(project);

        [HttpPut("team/{id}")]
        [Authorize("Delete")]
        [Authorize("Project")]
        public async Task<bool> DeleteTeam(SelectedDTO newTeam) => await _Project.DeleteTeam(newTeam);

        [HttpGet("download")]
        public async Task<IActionResult> Download() => File(await _Project.Download(), MimeType.XLSX);

        [HttpGet("team/download/{id}")]
        [Authorize("Project")]
        public async Task<IActionResult> DownloadTeam(long id) => File(await _Project.DownloadTeam(id), MimeType.XLSX);
    }
}
