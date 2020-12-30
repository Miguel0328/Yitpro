using AutoMapper;
using Repository.Interfaces;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EventService : IEventService
    {
        private readonly IEvent _event_;
        private readonly IMapper _mapper;

        public EventService(IEvent @event, IMapper mapper)
        {
            _event_ = @event;
            _mapper = mapper;
        }

        public async Task<List<EventDTO>> Get()
        {
            var events = await _event_.Get();
            return _mapper.Map<List<EventDTO>>(events);
        }
}
}
