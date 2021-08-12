using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Entities.Models
{
    public partial class JumpAppDbContext : DbContext
    {
        private IConfiguration _configuration;

        public JumpAppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JumpAppDbContext(DbContextOptions<JumpAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public virtual DbSet<CoreUserInformation> CoreUserInformation { get; set; }
        public virtual DbSet<UserInformation> UserInformation { get; set; }
        public virtual DbSet<WorkoutSession> WorkoutSession { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var myConnString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(myConnString);
               
            }
        }

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
        }
    }
}
