using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class PhaseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool PSP { get; set; }
        public bool Active { get; set; }
    }

    public class ClasificationDTO
    {
        public long PhaseId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
