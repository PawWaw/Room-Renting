using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class WSAddresses
    {
        public string address { get; set; }

        public string type { get; set; }

        public int bed_count { get; set; }

        public double price { get; set; }

        public double? rating { get; set; }

        public int? rateCount { get; set; }
    }
}
