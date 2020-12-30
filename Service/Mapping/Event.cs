using AutoMapper;
using Persistence.Models;
using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventModel, EventDTO>();
            CreateMap<EventDTO, EventModel>();
        }
    }
}
