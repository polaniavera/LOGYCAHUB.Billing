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
    
    public partial class CABASNET_PPAL_CO_V1_0Entities : DbContext
    {
        public CABASNET_PPAL_CO_V1_0Entities()
            : base("name=CABASNET_PPAL_CO_V1_0Entities")
        {
            // the terrible hack
            var ensureDLLIsCopied =
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CRM_EMPRESA> CRM_EMPRESA { get; set; }
        public virtual DbSet<EMPRESA> EMPRESA { get; set; }
    }
}