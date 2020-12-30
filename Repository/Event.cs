using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Event : IEvent
    {
        private readonly DataContext _context;

        public Event(DataContext context)
        {
            _context = context;
        }

        public async Task<List<EventModel>> Get()
        {
            var anniversaries =
                _context.User.Select(x => new EventModel
                {
                    Id = 0,
                    TypeId = EventType.Anniversary,
                    Title = "Aniversary",
                    StartDate = x.AdmissionDate,
                    EndDate = x.AdmissionDate
                });

            return await anniversaries.ToListAsync();
        }
    }
}
