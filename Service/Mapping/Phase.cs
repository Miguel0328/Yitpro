using AutoMapper;
using Persistence.Models;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class PhaseProfile : Profile
    {
        private readonly IBaseService _service;

        public PhaseProfile(IBaseService service)
        {
            _service = service;

            CreateMap<PhaseModel, PhaseDTO>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.PhaseId))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Phase.Description));
            CreateMap<PhaseDTO, PhaseModel>()
                .ForMember(x => x.PhaseId, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.PSP, o => o.MapFrom(s => s.PSP))
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

            CreateMap<PhaseModel, ClasificationDTO>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.ClasificationId))
                .ForMember(x => x.PhaseId, o => o.MapFrom(s => s.PhaseId))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Clasification.Description));
            CreateMap<ClasificationDTO, PhaseModel>()
                .ForMember(x => x.ClasificationId, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.PhaseId, o => o.MapFrom(s => s.PhaseId))
                .ForMember(x => x.Active, o => o.MapFrom(s => s.Active))
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));
        }
    }
}
