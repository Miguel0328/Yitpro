using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models;
using Resources.DTO;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profile;

        public ProfileController(IProfileService user)
        {
            _profile = user;
        }

        [HttpGet]
        public async Task<ActionResult<ProfileDTO>> Current() => await _profile.CurrentUser();        
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ProfileDTO>> Login(LoginDTO login) => await _profile.Login(login.Email, login.Password);
    }
}
