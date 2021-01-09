using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persistence;
using Resources.DTO;
using Resources.Extension;
using System;
using System.Collections.Generic;
using System.IO;
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
            var user = _context.User.FirstOrDefault(x => x.Email == email);

            var isProjectUser = user.GetProjects(_context).Select(x => x.Id).Contains(projectId);

            if (isProjectUser)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
