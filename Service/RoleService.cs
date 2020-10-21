using AutoMapper;
using Persistence.Models;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoleService : IRoleService
    {
        private readonly IRol _role;
        private readonly IBaseService _baseService;
        private readonly IMapper _mapper;

        public RoleService(IBaseService baseService, IMapper mapper, IRol role)
        {
            _role = role;
            _baseService = baseService;
            _mapper = mapper;
        }

        public async Task<bool> CreateRole(RoleDTO role)
        {
            var userId = _baseService.GetCurrentUserId();
            var roleModel = _mapper.Map<RoleDTO, RoleModel>(role);
            roleModel.CreatedById = userId;
            roleModel.CreatedAt = DateTime.Now;

            return await _role.CreateRole(roleModel);
        }

        public async Task<RoleDTO> ReadRole(int id)
        {
            var role = await _role.ReadRole(id);
            return _mapper.Map<RoleModel, RoleDTO>(role);
        }

        public async Task<List<RoleDTO>> ReadRoles()
        {
            var roles = await _role.ReadRoles();
            return _mapper.Map<List<RoleModel>, List<RoleDTO>>(roles);
        }

        public async Task<bool> UpdateRole(RoleDTO role)
        {
            var userId = _baseService.GetCurrentUserId();
            var roleModel = _mapper.Map<RoleDTO, RoleModel>(role);
            roleModel.UpdatedById = userId;
            roleModel.UpdatedAt = DateTime.Now;

            return await _role.UpdateRole(roleModel);
        }
    }
}
