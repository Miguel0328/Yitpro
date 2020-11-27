﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class UserPermissionsModel
    {
        public short MenuId { get; set; }
        public virtual MenuModel Menu { get; set; }
        public long UserId { get; set; }
        public virtual UserModel User { get; set; }
        public bool Access { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public long UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
