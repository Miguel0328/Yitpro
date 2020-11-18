using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public  class MenuModel
    {
        public short Id { get; set; }
        public short? ParentId { get; set; }
        public virtual MenuModel Parent { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        public bool Active { get; set; }
        public short Order { get; set; }
        public byte Level { get; set; }
        public string Icon { get; set; }
    }
}
