using Microsoft.EntityFrameworkCore;
using Oracle888730.OracleEF.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Oracle888730.OracleEF
{
    partial class OracleContext : DbContext
    {
        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //https://kontext.tech/column/dotnet_framework/275/sqlite-in-net-core-with-entity-framework-core
            optionsBuilder.UseSqlite("Filename=Oracle888730.sqlite", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }


    }
}
