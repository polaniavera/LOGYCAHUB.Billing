﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LOGYCAHUB.Billing.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CABASNET_TRAZABILIDADEntities : DbContext
    {
        public CABASNET_TRAZABILIDADEntities()
            : base("name=CABASNET_TRAZABILIDADEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AUDIT_MAESTRO> AUDIT_MAESTRO { get; set; }
        public virtual DbSet<PRICATS_X_EMPRESA> PRICATS_X_EMPRESA { get; set; }
        public virtual DbSet<TOKENS> TOKENS { get; set; }
        public virtual DbSet<USER_API> USER_API { get; set; }
        public virtual DbSet<MESSAGES> MESSAGES { get; set; }
    }
}
