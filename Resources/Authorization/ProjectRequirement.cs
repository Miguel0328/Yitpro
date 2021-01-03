using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Authorization
{
    public class ProjectRequirement : IAuthorizationRequirement
    {
    }

    public class ProjectRequirementHandler : AuthorizationHandler<ProjectRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public ProjectRequirementHandler(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectRequirement requirement)
        {
            var projectId = Convert.ToInt64(_httpContextAccessor.HttpContext.Request.RouteValues["id"]?.ToString());
            var email = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userId = _context.User.AsNoTracking().FirstOrDefault(x => x.Email == email)?.Id;

            var isProjectUser = _context.ProjectTeam.AsNoTracking().Any(x => x.ProjectId == projectId && x.UserId == userId && x.Active)
                || _context.Project.AsNoTracking().Any(x => x.Id == projectId && x.LeaderId == userId);

            if (isProjectUser)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
