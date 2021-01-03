using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPhaseService
    {
        Task<PhaseDTO> Get(long phaseId);
        Task<List<PhaseDTO>> Get();
        Task<List<ClasificationDTO>> GetClasifications(long phaseId);
        Task<bool> Put(ClasificationDTO phase);
        Task<bool> Put(long phaseId);
        Task<bool> Put(PhaseDTO phase);
    }
}
