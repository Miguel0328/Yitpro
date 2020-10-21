using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Protected { get; set; }
        public bool Active { get; set; }
        public long? CreatedById { get; set; }
        public UserModel CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? UpdatedById { get; set; }
        public UserModel UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
