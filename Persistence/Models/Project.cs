using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class ProjectModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public short ClientId { get; set; }
        public virtual ClientModel Client { get; set; }
        public long LeaderId { get; set; }
        public virtual UserModel Leader { get; set; }
        public long TypeId { get; set; }
        public virtual CatalogModel Type { get; set; }
        public long StatusId { get; set; }
        public virtual CatalogModel Status { get; set; }
        public long MethodologyId { get; set; }
        public virtual CatalogModel Methodology { get; set; }
        public bool PSP { get; set; }
        public bool Active { get; set; }
        public long UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<ProjectTeamModel> Team { get; set; }
        public virtual ICollection<ActivityModel> Activities { get; set; }
    }
}
