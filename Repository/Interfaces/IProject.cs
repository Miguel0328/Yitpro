using Persistence.Models;
using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IProject
    {
        Task<long> Post(ProjectModel _project);
        Task<bool> PostTeam(SelectedDTO _newTeam);
        Task<bool> Put(ProjectModel _project);
        Task<bool> DeleteTeam(SelectedDTO _deleteTeam);
        Task<bool> PutEnabled(ProjectModel _project);
        Task<List<ProjectModel>> Get(ProjectFilterDTO _filter, long userId);
        Task<List<ProjectTeamModel>> GetTeam(long id);
        Task<List<UserModel>> GetRemainingTeam(long id);
        Task<ProjectModel> GetDetail(long id);
        Task<long> GetId(string code);
        Task<object> Download(long userId);
        Task<object> DownloadTeam(long id);
    }
}
