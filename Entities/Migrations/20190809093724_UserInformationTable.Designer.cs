﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20190809093724_UserInformationTable")]
    partial class UserInformationTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.ActivityLog", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("ActiveDuration");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime");

                    b.HasKey("ActivityId");

                    b.ToTable("ActivityLog");
                });

            modelBuilder.Entity("Entities.Models.CoreUserInformation", b =>
                {
                    b.Property<int>("CoreUserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Dob")
                        .HasColumnName("DOB")
                        .HasColumnType("date");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<int>("Height");

                    b.Property<string>("LoginId")
                        .HasColumnName("LoginID")
                        .IsUnicode(false);

                    b.Property<int>("Sensetivity");

                    b.Property<int>("Weight");

                    b.HasKey("CoreUserId");

                    b.ToTable("CoreUserInformation");
                });

            modelBuilder.Entity("Entities.Models.Item", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Entities.Models.UserInformation", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Achievements")
                        .IsUnicode(false);

                    b.Property<int?>("Experience");

                    b.Property<bool?>("IsActive");

                    b.Property<int?>("Level");

                    b.Property<int?>("ProfileImageId");

                    b.HasKey("UserId");

                    b.ToTable("UserInformation");
                });

            modelBuilder.Entity("Entities.Models.WorkoutSession", b =>
                {
                    b.Property<int>("WorkoutSessionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AveragePace");

                    b.Property<int>("Colories");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("Experience");

                    b.Property<int>("TotalJumps");

                    b.HasKey("WorkoutSessionId");

                    b.ToTable("WorkoutSession");
                });
#pragma warning restore 612, 618
        }
    }
}