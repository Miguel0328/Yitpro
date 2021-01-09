using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class ActivityCommentModel
    {
        public long Id { get; set; }
        public long ActivityId { get; set; }
        public virtual ActivityModel Activity { get; set; }
        public long UserId { get; set; }
        public virtual UserModel User { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public bool Log { get; set; }
    }
}
