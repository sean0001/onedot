﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace one.OneDot.Areas.Stocks.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StockEntities : DbContext
    {
        public StockEntities()
            : base("name=StockEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<st_dayDataHistory> st_dayDataHistory { get; set; }
        public virtual DbSet<st_industryClassify> st_industryClassify { get; set; }
        public virtual DbSet<st_lastDayData> st_lastDayData { get; set; }
        public virtual DbSet<st_stock_basics_info> st_stock_basics_info { get; set; }
    }
}
