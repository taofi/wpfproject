﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SUBSCRIPTION_BDEntities : DbContext
    {
        private static SUBSCRIPTION_BDEntities _context;
        public SUBSCRIPTION_BDEntities()
            : base("name=SUBSCRIPTION_BDEntities")
        {
        }
    
        public static SUBSCRIPTION_BDEntities GetContext()
        {
            if(_context == null)
                _context = new SUBSCRIPTION_BDEntities();
            return _context;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AuthorPages> AuthorPages { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Subscription_type> Subscription_type { get; set; }
        public virtual DbSet<Subscriptions> Subscriptions { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}