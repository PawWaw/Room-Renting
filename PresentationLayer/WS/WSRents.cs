using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room_Renting.WS
{
    class WSRents
    {
        public long id { get; set; }
        public string address { get; set; }
        public string user { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public Nullable<short> rated { get; set; }
    }
}
