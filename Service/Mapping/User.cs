using AutoMapper;
using Microsoft.AspNetCore.Http;
using Persistence.Models;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class UserProfile : Profile
    {
        private readonly IBaseService _service;
        private readonly HttpRequest _request;

        public UserProfile(IBaseService baseService)
        {
            _service = baseService;
            _request = _service.GetRequest();

            CreateMap<UserModel, UserDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(s => $"{s.FirstName} {s.LastName} {s.SecondLastName}"))
                .ForMember(x => x.Role, o => o.MapFrom(s => s.Role.Name))
                .ForMember(x => x.Photo, o => o.MapFrom(s => s.Photo == null ? null :
                $"{_request.Scheme}://{_request.Host}{_request.PathBase}/{s.Photo}"));
            CreateMap<UserDTO, UserModel>()
                .ForMember(x => x.Role, o => o.Ignore())
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

            CreateMap<UserModel, UserDetailsDTO>()
                .ForMember(x => x.PhotoUrl, o => o.MapFrom(s => s.Photo == null ? null :
                $"{_request.Scheme}://{_request.Host}{_request.PathBase}/{s.Photo}"))
                .ForMember(x => x.Photo, o => o.Ignore());
            CreateMap<UserDetailsDTO, UserModel>()
                .ForMember(x => x.Photo, o => o.MapFrom(s => s.Photo == null ? null : s.PhotoUrl))
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

            CreateMap<UserPermissionsModel, UserPermisssionDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Menu.Description))
                .ForMember(x => x.Icon, o => o.MapFrom(s => s.Menu.Icon))
                .ForMember(x => x.Level, o => o.MapFrom(s => s.Menu.Level));
            CreateMap<UserPermisssionDTO, UserPermissionsModel>()
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));
        }
    }
}
