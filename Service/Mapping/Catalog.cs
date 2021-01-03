using AutoMapper;
using Persistence.Models;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class CatalogProfile : Profile
    {
        private readonly IBaseService _service;

        public CatalogProfile(IBaseService service)
        {
            _service = service;

            CreateMap<CatalogModel, CatalogDTO>();
            CreateMap<CatalogDTO, CatalogModel>()
                .ForMember(x => x.Protected, o => o.MapFrom(s => false))
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));
        }
    }
}
