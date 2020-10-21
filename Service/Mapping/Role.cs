using AutoMapper;
using Persistence.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleModel, RoleDTO>();
            CreateMap<RoleDTO, RoleModel>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name.Trim()));
        }
    }
}
