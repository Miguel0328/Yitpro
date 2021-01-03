using AutoMapper;
using Microsoft.AspNetCore.Http;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class SelectedProfile : Profile
    {
        private readonly IBaseService _service;
        private readonly HttpRequest _request;

        public SelectedProfile(IBaseService service)
        {
            _service = service;
            _request = _service.GetRequest();

            CreateMap<SelectedDTO, SelectedDTO>()
                .ForMember(x => x.UpdatedBy, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));
        }
    }
}
