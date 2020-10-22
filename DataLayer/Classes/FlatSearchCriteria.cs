using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class FlatSearchCriteria
    {
        public System.DateTime? startDate { get; set; }

        public System.DateTime? endDate { get; set; }

        public int? bedCount { get; set; }

        public int? price { get; set; }

        public bool over { get; set; }

        public string city { get; set; }

        public bool parking { get; set; }

        public bool kitchen { get; set; }

        public bool animals { get; set; }
    }
}
