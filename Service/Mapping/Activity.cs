using AutoMapper;
using Microsoft.AspNetCore.Http;
using Persistence.Models;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Mapping
{
    public class ActivityProfile : Profile
    {
        private readonly IBaseService _service;
        private readonly HttpRequest _request;

        public ActivityProfile(IBaseService service)
        {
            _service = service;
            _request = _service.GetRequest();

            CreateMap<ActivityModel, ActivityDetailDTO>()
                .ForMember(x => x.Period, o => o.MapFrom(s => new List<DateTime> { s.StartDate, s.EndDate }));
            CreateMap<ActivityDetailDTO, ActivityModel>()
                .ForMember(x => x.StartDate, o => o.MapFrom(s => s.Period.Min()))
                .ForMember(x => x.EndDate, o => o.MapFrom(s => s.Period.Max()))
                .ForMember(x => x.AssignedTime,
                    o => o.MapFrom(
                        s => (Convert.ToInt32(s.AssignedTime.Split(':', 2, StringSplitOptions.None).First()) * 3600)
                    + (Convert.ToInt32(s.AssignedTime.Split(':', 2, StringSplitOptions.None).Last()) * 60)))
                .ForMember(x => x.EstimatedTime,
                    o => o.MapFrom(
                        s => (Convert.ToInt32(s.EstimatedTime.Split(':', 2, StringSplitOptions.None).First()) * 3600)
                    + (Convert.ToInt32(s.EstimatedTime.Split(':', 2, StringSplitOptions.None).Last()) * 60)))
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

            CreateMap<ActivityModel, ActivityDTO>()
                .ForMember(x => x.Activity, o => o.MapFrom(s => s.Project.Code + "-" + s.Id))
                .ForMember(x => x.Status, o => o.MapFrom((s, d) =>
                {
                    return s.StatusId switch
                    {
                        1 => "Abierto",
                        2 => "En Progreso",
                        3 => "Revisión",
                        4 => "Verificada",
                        5 => "Liberada",
                        6 => "Rechazada",
                        7 => "Cancelada",
                        _ => "Indefinido",
                    };
                }))
                .ForMember(x => x.Phase, o => o.MapFrom(s => s.Phase.Alias + " - " + s.Clasification.Alias));
        }
    }
}
