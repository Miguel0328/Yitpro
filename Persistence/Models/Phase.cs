using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class PhaseModel
    {
        public long PhaseId { get; set; }
        public virtual CatalogModel Phase { get; set; }
        public long ClasificationId { get; set; }
        public virtual CatalogModel Clasification { get; set; }
        public bool PSP { get; set; }
        public bool Active { get; set; }
        public long UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
