using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapping
{
    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap<string, string>()
                .ConvertUsing(str => str == null ? null : str.Trim());
        }
    }
}
