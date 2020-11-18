using Microsoft.Extensions.Configuration;
using Persistence.Errors;
using Persistence.Models;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Service
{
    public class ProfileService : IProfileService
    {
        private readonly IProfile _profile_;
        private readonly IBaseService _baseService;
        private readonly string _secretPassword;

        public ProfileService(IConfiguration config, IProfile profile, IBaseService baseService)
        {
            _profile_ = profile;
            _baseService = baseService;
            _secretPassword = config["SecretPass"].ToString();
        }

        public List<MenuDTO> GetMenus(ICollection<RolePermissionsModel> permissions, ICollection<RolePermissionsModel> child, byte level)
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
            var id = _baseService.GetCurrentUserId();
            var user = await _profile_.CurrentUser(id);
            var permissions = user.Role.Permissions.Where(x => x.Access).ToList();
            var menus = GetMenus(permissions, permissions, 1);

            return new ProfileDTO
            {
                Name = user.FirstName,
                Menus = menus,
                Token = _baseService.CreateToken(user.Email)
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
                Token = _baseService.CreateToken(user.Email)
            };
        }
    }
}
