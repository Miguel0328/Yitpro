using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Permission
{
    public class ViewRequirement : IAuthorizationRequirement
    {
    }

    public class ViewRequirementHandler : AuthorizationHandler<ViewRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public ViewRequirementHandler(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewRequirement requirement)
        {
            var route = _httpContextAccessor.HttpContext.Request.RouteValues["controller"];
            var user = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var menu = _context.Menu.FirstOrDefault(x => x.Route == route.ToString().ToLower());

            var hasPermission = _context.RolePermissions.FirstOrDefault(x => x.RoleId == 1 && x.MenuId == menu.Id)?.Access;

            if (Convert.ToBoolean(hasPermission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
