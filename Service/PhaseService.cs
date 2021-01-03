using AutoMapper;
using Persistence.Models;
using Repository.Interfaces;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PhaseService : IPhaseService
    {
        private readonly IPhase _phase_;
        private readonly IMapper _mapper;
        private readonly IBaseService _service;

        public PhaseService(IPhase phase, IMapper mapper, IBaseService service)
        {
            _phase_ = phase;
            _mapper = mapper;
            _service = service;
        }

        public async Task<List<PhaseDTO>> Get()
        {
            var phases = await _phase_.Get();
            return _mapper.Map<List<PhaseDTO>>(phases);
        }        
        
        public async Task<PhaseDTO> Get(long phaseId)
        {
            var phase = (await _phase_.Get(phaseId)).FirstOrDefault();
            return _mapper.Map<PhaseDTO>(phase);
        }

        public async Task<List<ClasificationDTO>> GetClasifications(long phaseId)
        {
            var clasifications = await _phase_.GetClasifications(phaseId);
            return _mapper.Map<List<ClasificationDTO>>(clasifications);
        }

        public async Task<bool> Put(ClasificationDTO _phase)
        {
            var phase = _mapper.Map<PhaseModel>(_phase);
            return await _phase_.Put(phase);
        }        
        
        public async Task<bool> Put(PhaseDTO _phase)
        {
            var phase = _mapper.Map<PhaseModel>(_phase);
            return await _phase_.PutPSP(phase);
        }

        public async Task<bool> Put(long phaseId)
        {
            var userId = _service.GetCurrentUserId();
            return await _phase_.Put(phaseId, userId);
        }
    }
}
