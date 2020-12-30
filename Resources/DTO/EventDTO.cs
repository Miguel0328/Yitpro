using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class EventDTO
    {
        public short Id { get; set; }
        public byte TypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
