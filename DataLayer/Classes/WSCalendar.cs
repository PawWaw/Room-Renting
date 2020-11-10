using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class WSCalendar
    {
        public long id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string address { get; set; }
        public string client { get; set; }
    }
}
