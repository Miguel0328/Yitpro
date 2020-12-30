using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class ClientModel
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public long UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}
