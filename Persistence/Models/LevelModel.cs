using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class LevelModel
    {
        public long Id { get; set; }
        public string LevelName { get; set; }
        public decimal ComplianceFactor { get; set; }
        public decimal HourFactor { get; set; }
        public decimal Standard { get; set; }
        public decimal DailyStandard { get; set; }
        public decimal WeekStandard { get; set; }
        public bool Active { get; set; }
        public long IdUCreate { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
