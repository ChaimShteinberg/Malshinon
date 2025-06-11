using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    internal class UserReport
    {
        public string? Reporter { get; set; }
        public string? Target { get; set; }
        public string? ReportText { get; set; }
        public DateTime ReportingTime { get; set; }

        public UserReport()
        {
            this.ReportingTime = DateTime.Now;
        }


    }
}
