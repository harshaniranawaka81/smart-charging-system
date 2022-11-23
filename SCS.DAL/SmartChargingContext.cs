using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SCS.DAL
{
    public class SmartChargingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "SmartChargingSystemDB");
        }

        public SmartChargingContext(DbContextOptions<SmartChargingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<ChargeStation> ChargeStations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connector>()
            .HasOne<ChargeStation>()
            .WithMany()
            .HasForeignKey(c => c.RefChargeStationId)            
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChargeStation>()            
            .HasOne<Group>()
            .WithMany()            
            .HasForeignKey(c => c.RefGroupId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChargeStation>().HasAlternateKey(x => new { x.ChargeStationId, x.RefGroupId });
        }

    }
}
