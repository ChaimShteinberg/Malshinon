using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    internal class Alert
    {
        public int? Target { get; set; }
        public string Message { get; set; }

        public Alert(int? target, string message)
        {
            this.Target = target;
            this.Message = message;
        }
    }
}
