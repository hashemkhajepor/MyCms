﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyCms_DBEntities : DbContext
    {
        public MyCms_DBEntities()
            : base("name=MyCms_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<News_Groups> News_Groups { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<News_Selected_Groups> News_Selected_Groups { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
    }
}
