using AutoMapper;
using Microsoft.AspNetCore.Http;
using Persistence.Models;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class OptionProfile : Profile
    {
        private readonly IBaseService _service;
        private readonly HttpRequest _request;

        public OptionProfile(IBaseService service)
        {
            _service = service;
            _request = _service.GetRequest();

            CreateMap<RoleModel, OptionDTO>()
                .ForMember(x => x.Key, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(x => x.Text, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.Value, o => o.MapFrom(s => s.Id));            
            
            CreateMap<ClientModel, OptionDTO>()
                .ForMember(x => x.Key, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(x => x.Text, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.Value, o => o.MapFrom(s => s.Id));           
            
            CreateMap<CatalogModel, OptionDTO>()
                .ForMember(x => x.Key, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(x => x.Text, o => o.MapFrom(s => s.Alias))
                .ForMember(x => x.Value, o => o.MapFrom(s => s.Id));

            CreateMap<UserModel, OptionDTO>()
                .ForMember(x => x.Key, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(x => x.Text, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(x => x.Value, o => o.MapFrom(s => s.Id))
                .ForPath(x => x.Image.Avatar, o => o.MapFrom(s => true))
                .ForPath(x => x.Image.Src, o => o.MapFrom(s => s.Photo == null ?
                "/assets/avatar.png" :
                $"{_request.Scheme}://{_request.Host}{_request.PathBase}/{s.Photo}"));
        }
    }
}
