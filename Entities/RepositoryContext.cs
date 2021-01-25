using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Item> Items { get; set; }
        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public virtual DbSet<CoreUserInformation> CoreUserInformation { get; set; }
        public virtual DbSet<UserInformation> UserInformation { get; set; }
        public virtual DbSet<WorkoutSession> WorkoutSession { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Item>()
        //        .HasKey(entity => entity.Id)
        //        .HasName("PK_Id");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.HasKey(e => e.ActivityId);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.LoginId)
                    .HasColumnName("LoginID")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreUserInformation>(entity =>
            {
                entity.HasKey(e => e.CoreUserId);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LoginId)
                    .HasColumnName("LoginID")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserInformation>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Achievements).IsUnicode(false);

                entity.Property(e => e.LoginId)
                    .HasColumnName("LoginID")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorkoutSession>(entity =>
            {
                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasColumnName("LoginID")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);


            });
        }
    }
}
