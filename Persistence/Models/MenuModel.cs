using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public  class MenuModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public MenuModel Parent { get; set; }
        public string Description { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool Active { get; set; }
        public long Order { get; set; }
        public int Level { get; set; }
        public string Icon { get; set; }
    }
}
