//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rents
    {
        public int id { get; set; }
        public int address_id { get; set; }
        public int user_id { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public bool is_actual { get; set; }
        public bool pending { get; set; }
        public Nullable<short> rated { get; set; }
    
        public virtual Addresses Addresses { get; set; }
        public virtual Users Users { get; set; }
    }
}
