using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class RolePermissionModel
    {
        public short MenuId { get; set; }
        public virtual MenuModel Menu { get; set; }
        public short RoleId { get; set; }
        public virtual RoleModel Role { get; set; }
        public bool Access { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public long? UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
