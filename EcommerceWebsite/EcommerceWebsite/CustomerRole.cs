//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EcommerceWebsite
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerRole
    {
        public CustomerRole()
        {
            this.Customers = new HashSet<Customer>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
