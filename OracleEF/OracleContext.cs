using Microsoft.EntityFrameworkCore;
using Oracle888730.OracleEF.Models;
using System.Reflection;

namespace Oracle888730.OracleEF
{
    public partial class OracleContext : DbContext
    {
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //https://kontext.tech/column/dotnet_framework/275/sqlite-in-net-core-with-entity-framework-core
            optionsBuilder.UseSqlite("Filename=Oracle888730.sqlite", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Service>(x =>
            {
                x.HasIndex(e => e.ServiceName).IsUnique();
            });
            modelBuilder.Entity<ServiceType>().
                HasOne(x => x.Service).
                WithMany(x => x.ServiceTypes).
                HasForeignKey(x => x.ServiceForeignKey).
                HasConstraintName("FK_ServiceType_Service_ServiceId");
            modelBuilder.Entity<Subscriber>().
                HasOne(x => x.ServiceType).
                WithMany(x => x.Subscribers).
                HasForeignKey(x => x.ServiceTypeForeignKey).
                HasConstraintName("FK_Subscriber_ServiceType_ServiceTypeId");
        }
    }
}
