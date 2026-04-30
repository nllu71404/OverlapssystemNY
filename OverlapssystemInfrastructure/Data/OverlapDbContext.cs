using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlapssystemDomain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OverlapssystemInfrastructure.Data
{
    public class OverlapDbContext : IdentityDbContext<UserModel>
    {
        public OverlapDbContext(DbContextOptions<OverlapDbContext> options) : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserModel - fjern UserID, UserName og UserPassword da Identity håndterer dem
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasOne<DepartmentModel>()
                      .WithMany()
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // DepartmentModel forbliver uændret
            modelBuilder.Entity<DepartmentModel>(entity =>
            {
                entity.ToTable("Department", t => t.ExcludeFromMigrations());
                entity.HasKey(e => e.DepartmentID);
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
