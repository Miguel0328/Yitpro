using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class ActivityModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public virtual ProjectModel Project { get; set; }
        public long AssignedId { get; set; }
        public virtual UserModel Assigned { get; set; }
        public long ResponsibleId { get; set; }
        public virtual UserModel Responsible { get; set; }
        public long PhaseId { get; set; }
        public virtual CatalogModel Phase { get; set; }
        public long ClasificationId { get; set; }
        public virtual CatalogModel Clasification { get; set; }
        public int AssignedTime { get; set; }
        public int EstimatedTime { get; set; }
        public int FinalTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public string Requirement { get; set; }
        public string Description { get; set; }
        public bool Critical { get; set; }
        public bool Planned { get; set; }
        public bool Urgent { get; set; }
        public byte StatusId { get; set; }
        public long UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual List<ActivityCommentModel> Comments { get; set; }
    }
}
