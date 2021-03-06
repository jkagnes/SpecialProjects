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
    
    public partial class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
    
        public int Id { get; set; }
        public System.Guid OrderGuid { get; set; }
        public int CustomerId { get; set; }
        public int BillingAddressId { get; set; }
        public Nullable<int> ShippingAddressId { get; set; }
        public decimal OrderTotal { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Address Address1 { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
