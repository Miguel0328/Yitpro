using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class RoleModel
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public bool Protected { get; set; }
        public bool Active { get; set; }
        public long? UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<RolePermissionModel> Permissions { get; set; }
    }
}
