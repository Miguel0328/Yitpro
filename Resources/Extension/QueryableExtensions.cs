using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Resources.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resources.Extension
{
    public static class QueryableExtensions
    {
        public static IQueryable<ProjectModel> GetProjects(this UserModel user, DataContext context)
        {
            var roleId = user.RoleId;
            var departmentId = user.DepartmentId;
            var userId = user.Id;

            var projects = context.Project
                .Include(x => x.Team).ThenInclude(x => x.User)
                .Include(x => x.Leader)
                .Include(x => x.Client)
                .Include(x => x.Status)
                .Include(x => x.Type)
                .Include(x => x.Methodology)
                .Select(x => x);

            if (roleId == UserRole.Manager)
            {
                projects = projects
                    .Where(x => (x.Leader.DepartmentId == departmentId || x.Team.Where(y => x.Active).Select(y => y.User.DepartmentId).Contains(departmentId)));
            }
            else if (roleId != UserRole.Admin)
            {
                projects = projects
                    .Where(x => x.LeaderId == userId || x.Team.Where(y => y.Active).Select(x => x.UserId).Contains(userId));
            }

            return projects;
        }

        public static IQueryable<ActivityModel> GetActivities(this UserModel user, DataContext context)
        {
            var roleId = user.RoleId;
            var departmentId = user.DepartmentId;
            var userId = user.Id;
            var projects = user.GetProjects(context).Select(x => x.Id);

            var activities = context.Activity
                .Include(x => x.Responsible).ThenInclude(x => x.Role)
                .Include(x => x.Assigned).ThenInclude(x => x.Role)
                .Include(x => x.Project)
                .Include(x => x.Phase)
                .Include(x => x.Clasification)
                .Where(x => projects.Contains(x.ProjectId))
                .Select(x => x);

            if (roleId == UserRole.Manager)
            {
                activities = activities
                    .Where(x => x.Responsible.DepartmentId == departmentId || x.Assigned.DepartmentId == departmentId);
            }
            else if (roleId != UserRole.Admin)
            {
                activities = activities
                    .Where(x => x.Responsible.Id == userId || x.Assigned.Id == userId);
            }

            return activities;
        }
    }
}
