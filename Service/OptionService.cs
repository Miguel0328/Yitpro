﻿using AutoMapper;
using Persistence;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OptionService : IOptionService
    {
        private readonly IMapper _mapper;
        private readonly IOption _option_;

        public OptionService(IMapper mapper, IOption option)
        {
            _mapper = mapper;
            _option_ = option;
        }

        public async Task<List<OptionDTO>> GetRoles()
        {
            var options = await _option_.GetRoles();
            return _mapper.Map<List<OptionDTO>>(options);
        }        
        
        public async Task<List<OptionDTO>> GetLineManagers()
        {
            var options = await _option_.GetLineManagers();
            return _mapper.Map<List<OptionDTO>>(options);
        }
    }
}
