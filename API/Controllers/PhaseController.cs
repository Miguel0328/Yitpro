using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhaseController : ControllerBase
    {
        private readonly IPhaseService _phase;

        public PhaseController(IPhaseService phase)
        {
            _phase = phase;
        }

        [HttpGet("clasifications/{id}")]
        [Authorize("Get")]
        public async Task<List<ClasificationDTO>> GetClasifications(long id) => await _phase.GetClasifications(id);

        [Authorize("Get")]
        public async Task<List<PhaseDTO>> Get() => await _phase.Get();        
        
        [HttpGet("{id}")]
        [Authorize("Get")]
        public async Task<PhaseDTO> Get(long id) => await _phase.Get(id);

        [HttpPut]
        [Authorize("Put")]
        public async Task<bool> Put(ClasificationDTO phase) => await _phase.Put(phase);        
        
        [HttpPut("psp")]
        [Authorize("Put")]
        public async Task<bool> PutPSP(PhaseDTO phase) => await _phase.Put(phase);        
        
        [HttpPut("{id}")]
        [Authorize("Put")]
        public async Task<bool> Put(long id) => await _phase.Put(id);
    }
}
