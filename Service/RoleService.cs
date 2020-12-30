﻿using AutoMapper;
using Persistence.Models;
using Repository.Interfaces;
using Resources.DTO;
using Resources.Extension;
using Service.Interfaces;
using Resources.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoleService : IRoleService
    {
        private readonly IRole _role_;
        private readonly IMapper _mapper;

        public RoleService(IMapper mapper, IRole role)
        {
            _role_ = role;
            _mapper = mapper;
        }

        public async Task<short> Post(RoleDTO _role)
        {
            var role = _mapper.Map<RoleModel>(_role);
            return await _role_.Post(role);
        }

        public async Task<RoleDTO> Get(short id)
        {
            var role = await _role_.Get(id);
            return _mapper.Map<RoleDTO>(role);
        }

        public async Task<List<RoleDTO>> Get()
        {
            var roles = await _role_.Get();
            return _mapper.Map<List<RoleDTO>>(roles);
        }

        public async Task<bool> Put(RoleDTO _role)
        {
            var role = _mapper.Map<RoleModel>(_role);
            return await _role_.Put(role);
        }        

        public async Task<List<RolePermisssionDTO>> GetPermissions(short id)
        {
            var permissions = await _role_.GetPermissions(id);
            return _mapper.Map<List<RolePermisssionDTO>>(permissions);
        }

        public async Task<bool> PutPermissions(List<RolePermisssionDTO> _permissions)
        {
            var permissions = _mapper.Map<List<RolePermissionModel>>(_permissions);
            return await _role_.PutPermissions(permissions);
        }

        public async Task<byte[]> Download()
        {
            var roles = await _role_.Download();
            var file = roles.ToTable("Usuarios").ToExcel();

            return file;
        }
    }
}
