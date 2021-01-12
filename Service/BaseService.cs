using Microsoft.AspNetCore.Hosting;
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
using System.IO;
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
        private readonly IConfiguration _configuration;

        public BaseService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _configuration = configuration;
        }

        public string CreateToken(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, email),
            };

            var key = _key;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public long GetCurrentUserId()
        {
            var email = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var id = _context.User.AsNoTracking().SingleOrDefault(x => x.Email == email).Id;

            return id;
        }

        public string GetBasePath()
        {
            return _configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
        }

        public HttpRequest GetRequest()
        {
            return _httpContextAccessor.HttpContext.Request;
        }
    }
}
