using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.DTO
{
    public class SelectedDTO
    {
        public long Id { get; set; }
        public List<long> Ids { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long UpdatedBy { get; set; }
    }
}
