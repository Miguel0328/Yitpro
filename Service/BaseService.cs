using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BaseService : IBaseService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public BaseService(IConfiguration config, IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public string CreateToken(string employeeNumber)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, employeeNumber)
            };

            var key = _key;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> View(string controller, string view)
        {
            var user = await _context.User.SingleOrDefaultAsync(x => x.EmployeeNumber == GetCurrentUsername());
            var currentView = await _context.View.SingleOrDefaultAsync(x => x.UserId == user.Id);

            if (currentView == null)
            {
                _context.View.Add(new ViewModel
                {
                    UserId = user.Id,
                    View = view,
                    Controller = controller
                });
            }
            else
            {
                currentView.UserId = user.Id;
                currentView.View = view;
                currentView.Controller = controller;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public string GetCurrentUsername()
        {
            var employeeNumber = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return employeeNumber;
        }
    }
}
