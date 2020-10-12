using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class UserTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Protected { get; set; }
        public bool Active { get; set; }
        public long UserCreateId { get; set; }
        public DateTime DateCreate { get; set; }
        public long UserModifiedId { get; set; }
        public DateTime DateModified { get; set; }
    }
}
