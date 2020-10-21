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
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Rents = new HashSet<Rents>();
            this.Reviews = new HashSet<Reviews>();
            this.UserAddresses = new HashSet<UserAddresses>();
        }
    
        public long id { get; set; }
        public long personal_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email_addr { get; set; }
        public string acc_type { get; set; }
        public bool is_active { get; set; }
        public bool is_banned { get; set; }
        public bool verified { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<System.DateTime> modify_date { get; set; }
        public Nullable<System.DateTime> disable_date { get; set; }
        public Nullable<short> rating { get; set; }
        public string salt { get; set; }
    
        public virtual PersonalData PersonalData { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rents> Rents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> Reviews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAddresses> UserAddresses { get; set; }
    }
}