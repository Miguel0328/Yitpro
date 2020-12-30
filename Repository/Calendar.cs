using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class Calendar
    {
        private readonly DataContext _context;

        public Calendar(DataContext context)
        {
            _context = context;
        }
    }
}
