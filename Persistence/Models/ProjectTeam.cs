using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class ProjectTeamModel
    {
        public long UserId { get; set; }
        public virtual UserModel User { get; set; }
        public long ProjectId { get; set; }
        public virtual ProjectModel Project { get; set; }
        public bool Active { get; set; }
        public long UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
