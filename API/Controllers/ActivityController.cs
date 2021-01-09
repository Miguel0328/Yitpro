using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activity;

        public ActivityController(IActivityService activity)
        {
            _activity = activity;
        }

        [HttpPost]
        public async Task<long> Post(ActivityDetailDTO activity) => await _activity.Post(activity);

        [HttpGet]
        public async Task<List<ActivityDTO>> Get() => await _activity.Get();

        [HttpGet("detail/{id}")]
        public async Task<ActivityDetailDTO> GetDetail(long id) => await _activity.GetDetail(id);
    }
}
