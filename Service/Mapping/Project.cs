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
    public class ProjectProfile : Profile
    {
        private readonly IBaseService _service;
        private readonly HttpRequest _request;

        public ProjectProfile(IBaseService service)
        {
            _service = service;
            _request = _service.GetRequest();

            CreateMap<ProjectModel, ProjectDTO>()
                .ForMember(x => x.Client, o => o.MapFrom(s => s.Client.Name))
                .ForMember(x => x.Leader, o => o.MapFrom(s => s.Leader.FullName))
                .ForMember(x => x.LeaderPhoto, o => o.MapFrom(s => s.Leader.Photo == null ? null :
                $"{_request.Scheme}://{_request.Host}{_request.PathBase}/{s.Leader.Photo}"));
            CreateMap<ProjectDTO, ProjectModel>()
                .ForMember(x => x.Client, o => o.Ignore())
                .ForMember(x => x.Leader, o => o.Ignore())
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

            CreateMap<ProjectModel, ProjectDetailDTO>();
            CreateMap<ProjectDetailDTO, ProjectModel>()
                .ForMember(x => x.UpdatedById, o => o.MapFrom(s => _service.GetCurrentUserId()))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(s => DateTime.Now));

            CreateMap<ProjectTeamModel, ProjectTeamDTO>()
                .ForMember(x => x.User, o => o.MapFrom(s => s.User.FullName))
                .ForMember(x => x.UserPhoto, o => o.MapFrom(s => s.User.Photo == null ? null :
                $"{_request.Scheme}://{_request.Host}{_request.PathBase}/{s.User.Photo}"));
        }
    }
}
