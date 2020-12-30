using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDTO>> Get();
    }
}
