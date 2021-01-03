using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDTO>> Get(ProjectFilterDTO _filter = null);
        Task<ProjectDTO> Get(long id);
        Task<List<ProjectTeamDTO>> GetTeam(long id);
        Task<List<UserDTO>> GetRemainingTeam(long id);
        Task<long> GetId(string code);
        Task<ProjectDetailDTO> GetDetail(long id);
        Task<long> Post(ProjectDetailDTO project);
        Task<bool> PostTeam(SelectedDTO _newTeam);
        Task<bool> Put(ProjectDetailDTO project);
        Task<bool> PutEnabled(ProjectDTO project);
        Task<bool> DeleteTeam(SelectedDTO _deleteTeam);
        Task<byte[]> Download();
        Task<byte[]> DownloadTeam(long id);
    }
}
