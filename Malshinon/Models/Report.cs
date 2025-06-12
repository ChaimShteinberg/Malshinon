using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    internal class Report
    {
        public int ReportID { get; set; }
        public int? Reporter { get; set; }
        public int? Target { get; set; }
        public string? ReportText { get; set; }
        public DateTime ReportingTime { get; set; }
    }
}
