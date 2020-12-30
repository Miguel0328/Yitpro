using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IEvent
    {
        Task<List<EventModel>> Get();
    }
}
