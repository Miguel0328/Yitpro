using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class GeneralCatalogModel
    {
        public long Id { get; set; }
        public int CatalogId { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string SpecialData { get; set; }
        public bool Header { get; set; }
        public bool Protected { get; set; }
        public bool Active { get; set; }
        public long IdUCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public long IdUModified { get; set; }
        public DateTime DateModified { get; set; }
    }
}
