using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class MenuDTO
    {
        public long MenuId { get; set; }
        public long RoleId { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        public byte Level { get; set; }
        public string Icon { get; set; }
        public List<MenuDTO> Submenus { get; set; }
    }
}
