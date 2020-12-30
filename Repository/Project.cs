using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.DTO;
using Resources.Errors;
using Resources.Extension;
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

        public async Task<List<ProjectModel>> Get(ProjectFilterDTO filter)
        {
            var projects = _context.Project.Include(x => x.Leader).Include(x => x.Client).Select(x => x);

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
            var team = await _context.ProjectTeam.Include(x => x.User).Where(x => x.ProjectId == id && x.Active).ToListAsync();
            return team;
        }

        public async Task<ProjectModel> GetDetail(long id)
        {
            var project = await _context.Project.Include(x => x.Leader).Include(x => x.Client).FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
                throw new RestException(HttpStatusCode.NotFound, new { project = "Not found" });

            return project;
        }        
        
        public async Task<long> GetId(string code)
        {
            var project = await _context.Project.FirstOrDefaultAsync(x => x.Code == code);
            if (project == null)
                throw new RestException(HttpStatusCode.NotFound, new { project = "Not found" });

            return project.Id;
        }

        public async Task<long> Post(ProjectModel project)
        {
            var duplicate = await _context.Project.AnyAsync(x => x.Name == project.Name || x.Code == project.Code);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { project = "Already exists" });

            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task<bool> Put(ProjectModel _project)
        {
            var project = await _context.Project.AsNoTracking().FirstOrDefaultAsync(x => x.Id == _project.Id);
            if (project == null)
                throw new RestException(HttpStatusCode.NotFound, new { project = "Not found" });

            var duplicate = await _context.Project
                .AnyAsync(x => (x.Name == _project.Name || x.Code == _project.Code) && x.Id != _project.Id);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { project = "Already exists" });

            var entry = _context.Entry(_project);
            entry.State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> PutEnabled(ProjectModel _project)
        {
            var project = await _context.Project.AsNoTracking().FirstOrDefaultAsync(x => x.Id == _project.Id);
            if (project == null)
                throw new RestException(HttpStatusCode.NotFound, new { project = "Not found" });

            var entry = _context.Attach(_project);
            entry.Property(x => x.Active).IsModified = true;
            entry.Property(x => x.UpdatedById).IsModified = true;
            entry.Property(x => x.UpdatedAt).IsModified = true;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<object> Download()
        {
            var projects =
                await _context.Project
                .Select(x => new
                {
                    Clave = x.Code,
                    Proyecto = x.Name,
                    Cliente = x.Client.Name,
                    Lider = x.Leader.FullName,
                    Descripcion = x.Description.SplitWords(10),
                    Activo = x.Active ? "Sí" : "No"
                }).ToListAsync();

            if (projects.Count == 0)
                throw new RestException(HttpStatusCode.NotFound, new { Usuarios = "No hay registros para exportar" });

            return projects;
        }
    }
}
