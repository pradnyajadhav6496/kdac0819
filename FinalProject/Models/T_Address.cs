//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Address
    {
        public int AddressId { get; set; }
        public string State { get; set; }
        public string AddressLine { get; set; }
        public string Landmark { get; set; }
        public string Pincode { get; set; }
        public int UserId { get; set; }
    
        public virtual T_Users T_Users { get; set; }
        public virtual T_PIN T_PIN { get; set; }
    }
}
