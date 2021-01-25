using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Models
{
    public partial class JumpAppDbContext : DbContext
    {
        public JumpAppDbContext()
        {
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:jumpappdbserver.database.windows.net,1433;Initial Catalog=JumpAppDb;Persist Security Info=False;User ID=rstaszczak;Password=Staszczak100;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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
