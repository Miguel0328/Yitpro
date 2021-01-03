using AutoMapper;
using Persistence.Models;
using Repository.Interfaces;
using Resources.DTO;
using Resources.Extension;
using Resources.Reports;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProject _project_;
        private readonly IBaseService _service;

        public ProjectService(IMapper mapper, IProject project, IBaseService service)
        {
            _mapper = mapper;
            _service = service;
            _project_ = project;
        }

        public async Task<List<ProjectDTO>> Get(ProjectFilterDTO filter)
        {
            var userId = _service.GetCurrentUserId();
            var projects = await _project_.Get(filter, userId);
            return _mapper.Map<List<ProjectDTO>>(projects);
        }

        public async Task<ProjectDTO> Get(long id)
        {
            var project = await _project_.GetDetail(id);
            return _mapper.Map<ProjectDTO>(project);
        }               
        
        public async Task<List<ProjectTeamDTO>> GetTeam(long id)
        {
            var team = await _project_.GetTeam(id);
            return _mapper.Map<List<ProjectTeamDTO>>(team);
        }          
        
        public async Task<List<UserDTO>> GetRemainingTeam(long id)
        {
            var remaining = await _project_.GetRemainingTeam(id);
            return _mapper.Map<List<UserDTO>>(remaining);
        }        
        
        public async Task<long> GetId(string code)
        {
            var id = await _project_.GetId(code);
            return id;
        }

        public async Task<ProjectDetailDTO> GetDetail(long id)
        {
            var project = await _project_.GetDetail(id);
            return _mapper.Map<ProjectDetailDTO>(project);
        }

        public async Task<long> Post(ProjectDetailDTO _project)
        {
            var project = _mapper.Map<ProjectModel>(_project);
            return await _project_.Post(project);
        }        
        
        public async Task<bool> PostTeam(SelectedDTO _newTeam)
        {
            var newTeam = _mapper.Map<SelectedDTO>(_newTeam);
            return await _project_.PostTeam(newTeam);
        }

        public async Task<bool> Put(ProjectDetailDTO _project)
        {
            var project = _mapper.Map<ProjectModel>(_project);
            return await _project_.Put(project);
        }

        public async Task<bool> PutEnabled(ProjectDTO _project)
        {
            var project = _mapper.Map<ProjectModel>(_project);
            return await _project_.PutEnabled(project);
        }

        public async Task<bool> DeleteTeam(SelectedDTO _deleteTeam)
        {
            var deleteTeam = _mapper.Map<SelectedDTO>(_deleteTeam);
            return await _project_.DeleteTeam(deleteTeam);
        }

        public async Task<byte[]> Download()
        {
            var userId = _service.GetCurrentUserId();
            var projects = await _project_.Download(userId);
            var file = projects.ToTable("Usuarios").ToExcel();

            return file;
        }

        public async Task<byte[]> DownloadTeam(long id)
        {
            var projects = await _project_.DownloadTeam(id);
            var file = projects.ToTable("Equipo").ToExcel();

            return file;
        }
    }
}
