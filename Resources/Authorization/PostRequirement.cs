﻿using Microsoft.AspNetCore.Authorization;
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
    public class PostRequirement : IAuthorizationRequirement
    {
    }

    public class PostRequirementHandler : AuthorizationHandler<PostRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public PostRequirementHandler(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostRequirement requirement)
        {
            var route = _httpContextAccessor.HttpContext.Request.RouteValues["controller"];
            var email = _httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userId = _context.User.AsNoTracking().FirstOrDefault(x => x.Email == email)?.Id;
            var menu = _context.Menu.AsNoTracking().FirstOrDefault(x => x.Route == route.ToString().ToLower());

            var hasPermission = _context.UserPermission.FirstOrDefault(x => x.UserId == userId && x.MenuId == menu.Id)?.Create;

            if (Convert.ToBoolean(hasPermission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
