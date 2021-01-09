using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Resources.Extension;
using Repository.Interfaces;
using Resources.Constants;
using Resources.DTO;
using Resources.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Project : IProject
    {
        private readonly DataContext _context;

        public Project(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectModel>> Get(ProjectFilterDTO filter, long userId)
        {
            var user = await _context.User.FindAsync(userId);
            var projects = user.GetProjects(_context);

            if (filter != null)
            {
                projects = projects.Where(x =>
                (x.Name.Contains(filter.Name) || filter.Name == "")
                && (x.Code.Contains(filter.Code) || filter.Code == "")
                && (x.ClientId == filter.ClientId || filter.ClientId == null)
                && (x.Active == filter.Active || filter.Active == null));
            }

            return await projects.ToListAsync();
        }

        public async Task<List<ProjectTeamModel>> GetTeam(long id)
        {
            return await _context.ProjectTeam.Include(x => x.User).Where(x => x.ProjectId == id && x.Active).ToListAsync();
        }

        public async Task<List<UserModel>> GetRemainingTeam(long id)
        {
            var current = _context.ProjectTeam.Where(x => x.ProjectId == id && x.Active).Select(x => x.User);
            var all = _context.User.Select(x => x);
            var remaining = await all.Except(current).ToListAsync();
            return remaining;
        }

        public async Task<ProjectModel> GetDetail(long id)
        {
            return await _context.Project
                .Include(x => x.Leader).Include(x => x.Client).Include(x => x.Status).Include(x => x.Type).Include(x => x.Methodology)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> GetId(string code)
        {
            var project = await _context.Project.FirstOrDefaultAsync(x => x.Code == code);
            if (project == null)
                throw new RestException(HttpStatusCode.NotFound, new { project = Messages.NotFound });

            return project.Id;
        }

        public async Task<long> Post(ProjectModel project)
        {
            var duplicate = await _context.Project.AnyAsync(x => x.Name == project.Name || x.Code == project.Code);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { project = Messages.Duplicated });

            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task<bool> PostTeam(SelectedDTO _newTeam)
        {
            var usersToAdd = _context.User.Where(x => _newTeam.Ids.Contains(x.Id));

            var toUpdate = _context.ProjectTeam
                .Where(x => x.ProjectId == _newTeam.Id && _newTeam.Ids.Contains(x.UserId))
                .Select(x => new ProjectTeamModel
                {
                    ProjectId = x.ProjectId,
                    UserId = x.UserId,
                    Active = true,
                    UpdatedById = _newTeam.UpdatedBy,
                    UpdatedAt = _newTeam.UpdatedAt
                });

            var toAdd = usersToAdd.Select(x => new ProjectTeamModel
            {
                ProjectId = _newTeam.Id,
                UserId = x.Id,
                Active = true,
                UpdatedById = _newTeam.UpdatedBy,
                UpdatedAt = _newTeam.UpdatedAt
            });

            var union = await toAdd.Union(toUpdate).ToListAsync();
            await _context.BulkInsertOrUpdateAsync(union);

            return true;
        }

        public async Task<bool> Put(ProjectModel _project)
        {
            var duplicate = await _context.Project
                .FirstOrDefaultAsync(x => (x.Name == _project.Name || x.Code == _project.Code) && x.Id != _project.Id) != null;
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { project = Messages.Duplicated });

            var entry = _context.Entry(_project);
            entry.State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> PutEnabled(ProjectModel _project)
        {
            var entry = _context.Attach(_project);
            entry.Property(x => x.Active).IsModified = true;
            entry.Property(x => x.UpdatedById).IsModified = true;
            entry.Property(x => x.UpdatedAt).IsModified = true;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTeam(SelectedDTO _deleteTeam)
        {
            var toUpdate = await _context.ProjectTeam
                .Where(x => x.ProjectId == _deleteTeam.Id && _deleteTeam.Ids.Contains(x.UserId))
                .Select(x => new ProjectTeamModel
                {
                    ProjectId = x.ProjectId,
                    UserId = x.UserId,
                    Active = false,
                    UpdatedById = _deleteTeam.UpdatedBy,
                    UpdatedAt = _deleteTeam.UpdatedAt
                }).ToListAsync();

            await _context.BulkUpdateAsync(toUpdate);

            return true;
        }

        public async Task<object> Download(long userId)
        {
            var projects =
                await _context.Project
                .Include(x => x.Team).Include(x => x.Leader).Include(x => x.Client).Include(x => x.Status).Include(x => x.Type).Include(x => x.Methodology)
                .Where(x => x.LeaderId == userId || x.Team.Where(y => y.Active).Select(x => x.UserId).Contains(userId))
                .Select(x => new
                {
                    Clave = x.Code,
                    Proyecto = x.Name,
                    Cliente = x.Client.Name,
                    Lider = x.Leader.FullName,
                    Tipo = x.Type.Description,
                    Metodologia = x.Methodology.Description,
                    Estatus = x.Status.Description,
                    Descripcion = x.Description.SplitWords(),
                    Activo = x.Active ? "Sí" : "No"
                }).ToListAsync();

            if (projects.Count == 0)
                throw new RestException(HttpStatusCode.NotFound, new { Usuarios = Messages.NothingToExport });

            return projects;
        }

        public async Task<object> DownloadTeam(long id)
        {
            var projects =
                await _context.ProjectTeam.Include(x => x.User).Include(x => x.Project)
                .Where(x => x.ProjectId == id && x.Active)
                .Select(x => new
                {
                    Proyecto = x.Project.Name,
                    Usuario = x.User.FullName,
                    Incluido_desde = x.UpdatedAt.ToString("dd/MM/yyyy")
                }).ToListAsync();

            if (projects.Count == 0)
                throw new RestException(HttpStatusCode.NotFound, new { Usuarios = Messages.NothingToExport });

            return projects;
        }
    }
}
