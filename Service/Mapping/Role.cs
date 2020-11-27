using AutoMapper;
using Persistence.Models;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class RoleProfile : Profile
    {
        private readonly IBaseService _service;

        public RoleProfile(IBaseService baseService)
        {
            _service = baseService;

            CreateMap<RoleModel, RoleDTO>();
            CreateMap<RoleDTO, RoleModel>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name.Trim()))
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

            CreateMap<RolePermissionsModel, RolePermisssionDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Menu.Description))
                .ForMember(x => x.Icon, o => o.MapFrom(s => s.Menu.Icon))
                .ForMember(x => x.Level, o => o.MapFrom(s => s.Menu.Level));
            CreateMap<RolePermisssionDTO, RolePermissionsModel>()
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

        }
    }
}
