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
        private readonly IBaseService _baseService;

        public RoleProfile(IBaseService baseService)
        {
            _baseService = baseService;

            CreateMap<RoleModel, RoleDTO>();
            CreateMap<RoleDTO, RoleModel>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name.Trim()))
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _baseService.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

            CreateMap<RolePermissionsModel, RolePermisssionDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Menu.Description));
            CreateMap<RolePermisssionDTO, RolePermissionsModel>()
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _baseService.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

        }
    }
}
