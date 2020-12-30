using Microsoft.Extensions.Configuration;
using Resources.Errors;
using Persistence.Models;
using Repository.Interfaces;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Service
{
    public class ProfileService : IProfileService
    {
        private readonly IProfile _profile_;
        private readonly IBaseService _service;
        private readonly string _secretPassword;
        private readonly HttpRequest _request;

        public ProfileService(IConfiguration configuration, IProfile profile, IBaseService service)
        {
            _profile_ = profile;
            _service = service;
            _secretPassword = configuration["SecretPass"].ToString();
            _request = _service.GetRequest();
        }

        public List<MenuDTO> GetMenus(ICollection<UserPermissionModel> permissions, ICollection<UserPermissionModel> child, byte level)
        {
            var menus = new List<MenuDTO>();
            foreach (var menu in child.Where(x => x.Menu.Level == level))
            {
                menus.Add(new MenuDTO
                {
                    MenuId = menu.MenuId,
                    Description = menu.Menu.Description,
                    Level = menu.Menu.Level,
                    Icon = menu.Menu.Icon,
                    Route = menu.Menu.Route,
                    Submenus = GetMenus(permissions, permissions.Where(x => x.Menu.ParentId == menu.MenuId).ToList(), Convert.ToByte(level + 1))
                });
            }
            return menus;
        }

        public async Task<ProfileDTO> CurrentUser()
        {
            var id = _service.GetCurrentUserId();
            var user = await _profile_.CurrentUser(id);
            var permissions = user.Permissions.Where(x => x.Access).ToList();
            var menus = GetMenus(permissions, permissions, 1);

            return new ProfileDTO
            {
                Name = user.FirstName,
                Photo = user.Photo == null ? null : $"{_request.Scheme}://{_request.Host}{_request.PathBase}/{user.Photo}",
                Menus = menus,
                Token = _service.CreateToken(user.Email)
            };
        }

        public async Task<ProfileDTO> Login(string email, string password)
        {
            var user = await _profile_.Login(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password + _secretPassword, user.Password))
                throw new RestException(HttpStatusCode.Unauthorized);

            return new ProfileDTO
            {
                Name = user.FirstName,
                Token = _service.CreateToken(user.Email)
            };
        }
    }
}
