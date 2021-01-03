using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class CatalogModel
    {
        public long Id { get; set; }
        public long? CatalogId { get; set; }
        public virtual CatalogModel Catalog { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public bool Protected { get; set; }
        public bool Active { get; set; }
        public long? UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
