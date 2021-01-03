using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPhase
    {
        Task<List<PhaseModel>> Get(long phaseId = 0);
        Task<List<PhaseModel>> GetClasifications(long phaseId);
        Task<bool> Put(PhaseModel phase);
        Task<bool> PutPSP(PhaseModel phase);
        Task<bool> Put(long phaseId, long userId);
    }
}
