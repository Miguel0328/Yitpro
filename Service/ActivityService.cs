using AutoMapper;
using Persistence.Models;
using Repository.Interfaces;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ActivityService : IActivityService
    {
        private readonly IMapper _mapper;
        private readonly IActivity _activity_;
        private readonly IBaseService _base;

        public ActivityService(IMapper mapper, IActivity activity, IBaseService @base)
        {
            _mapper = mapper;
            _activity_ = activity;
            _base = @base;
        }

        public async Task<List<ActivityDTO>> Get()
        {
            var userId = _base.GetCurrentUserId();
            var activities = await _activity_.Get(userId);
            return _mapper.Map<List<ActivityDTO>>(activities);
        }

        public async Task<ActivityDetailDTO> GetDetail(long id)
        {
            var activity = await _activity_.GetDetail(id);
            return _mapper.Map<ActivityDetailDTO>(activity);
        }

        public async Task<long> Post(ActivityDetailDTO _activity)
        {
            var activity = _mapper.Map<ActivityModel>(_activity);
            activity.StatusId = 1;
            return await _activity_.Post(activity);
        }
    }
}
