using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;

namespace OverlapssystemInfrastructure.Data
{
    public class OverlapDbContext : DbContext
    {
        public OverlapDbContext(DbContextOptions<OverlapDbContext> options) : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Konfigurér UserModel
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.UserPassword).IsRequired();
                entity.HasOne<DepartmentModel>()
                      .WithMany()
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
            // Konfigurér DepartmentModel
            modelBuilder.Entity<DepartmentModel>(entity =>
            {
                entity.ToTable("Department", t => t.ExcludeFromMigrations()); //Finder den eksisterende DepartmentModel og navngiver den "Departments" i databasen
                entity.HasKey(e => e.DepartmentID);
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
