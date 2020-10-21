using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class RolePermissionsModel
    {
        public long Id { get; set; }
        public int MenuId { get; set; }
        public MenuModel Menu { get; set; }
        public int RoleId { get; set; }
        public RoleModel Role { get; set; }
        public bool Watch { get; set; }
        public bool Save { get; set; }
        public bool Modify { get; set; }
        public bool Print { get; set; }
        public bool Delete { get; set; }
    }
}
