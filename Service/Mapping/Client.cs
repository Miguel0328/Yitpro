using AutoMapper;
using Persistence.Models;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class ClientProfile : Profile
    {
        private readonly IBaseService _service;

        public ClientProfile(IBaseService service)
        {
            _service = service;

            CreateMap<ClientModel, ClientDTO>()
                .ForMember(x => x.ProjectCount, o => o.MapFrom(s => s.Projects.Count));
            CreateMap<ClientDTO, ClientModel>()
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));
        }
    }
}
