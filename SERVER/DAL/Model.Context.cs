﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class neighboorAidDBEntities : DbContext
    {
        public neighboorAidDBEntities()
            : base("name=neighboorAidDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<CasesToDoctor> CasesToDoctors { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<HelpCall> HelpCalls { get; set; }
        public virtual DbSet<Keyword> Keywords { get; set; }
        public virtual DbSet<KeywordsToCase> KeywordsToCases { get; set; }
    }
}
