using AutoMapper;
using Persistence;
using Repository.Interfaces;
using Resources.DTO;
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
        private readonly IBaseService _base;

        public OptionService(IMapper mapper, IOption option, IBaseService @base)
        {
            _mapper = mapper;
            _option_ = option;
            _base = @base;
        }

        public async Task<List<OptionDTO>> GetRoles()
        {
            var options = await _option_.GetRoles();
            return _mapper.Map<List<OptionDTO>>(options);
        }          
        
        public async Task<List<OptionDTO>> GetClients()
        {
            var options = await _option_.GetClients();
            return _mapper.Map<List<OptionDTO>>(options);
        }                     
        
        public async Task<List<OptionDTO>> GetResponsibles()
        {
            var options = await _option_.GetResponsibles();
            return _mapper.Map<List<OptionDTO>>(options);
        }                    
        
        public async Task<List<OptionDTO>> GetProjects()
        {
            var userId = _base.GetCurrentUserId();
            var options = await _option_.GetProjects(userId);
            return _mapper.Map<List<OptionDTO>>(options);
        }               
        
        public async Task<List<OptionDTO>> GetCatalogs()
        {
            var options = await _option_.GetCatalogs();
            return _mapper.Map<List<OptionDTO>>(options);
        }               
        
        public async Task<List<OptionDTO>> GetCatalogs(long id)
        {
            var options = await _option_.GetCatalogs(id);
            return _mapper.Map<List<OptionDTO>>(options);
        }               
        
        public async Task<List<OptionDTO>> GetProjectTeam(long id)
        {
            var options = await _option_.GetProjectTeam(id);
            return _mapper.Map<List<OptionDTO>>(options);
        }                
        
        public async Task<List<OptionDTO>> GetClasifications(long id)
        {
            var options = await _option_.GetClasifications(id);
            return _mapper.Map<List<OptionDTO>>(options);
        }        
        
        public async Task<List<OptionDTO>> GetManagers()
        {
            var options = await _option_.GetManagers();
            return _mapper.Map<List<OptionDTO>>(options);
        }
    }
}
