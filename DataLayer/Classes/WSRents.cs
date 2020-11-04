using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room_Renting.WS
{
    public class WSRents
    {
        public long id { get; set; }
        public long addressId { get; set; }
        public string address { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public int? rate { get; set; }
    }
}
