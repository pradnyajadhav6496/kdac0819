﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FinalProjectDbEntities : DbContext
    {
        public FinalProjectDbEntities()
            : base("name=FinalProjectDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_ErrorLogs> T_ErrorLogs { get; set; }
        public virtual DbSet<T_OTP_Details> T_OTP_Details { get; set; }
        public virtual DbSet<T_PasswordHistoryLog> T_PasswordHistoryLog { get; set; }
        public virtual DbSet<T_Roles> T_Roles { get; set; }
        public virtual DbSet<T_Users> T_Users { get; set; }
        public virtual DbSet<T_Address> T_Address { get; set; }
        public virtual DbSet<T_Category> T_Category { get; set; }
        public virtual DbSet<T_Comments> T_Comments { get; set; }
        public virtual DbSet<T_OrderDetails> T_OrderDetails { get; set; }
        public virtual DbSet<T_Orders> T_Orders { get; set; }
        public virtual DbSet<T_Payments> T_Payments { get; set; }
        public virtual DbSet<T_PIN> T_PIN { get; set; }
        public virtual DbSet<T_Products> T_Products { get; set; }
        public virtual DbSet<T_ShoppingCart> T_ShoppingCart { get; set; }
    }
}
