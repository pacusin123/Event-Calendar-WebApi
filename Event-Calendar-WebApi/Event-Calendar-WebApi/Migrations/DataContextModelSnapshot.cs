﻿// <auto-generated />
using System;
using Event_Calendar_WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Event_Calendar_WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Event_Calendar_WebApi.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("RoleId");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            Name = "UserLocal"
                        });
                });

            modelBuilder.Entity("Event_Calendar_WebApi.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ScheduleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Schedule", (string)null);

                    b.HasData(
                        new
                        {
                            ScheduleId = 1,
                            Name = "Schedule 1",
                            UserId = 1
                        },
                        new
                        {
                            ScheduleId = 2,
                            Name = "Schedule 2",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Event_Calendar_WebApi.Models.ScheduleEvent", b =>
                {
                    b.Property<int>("ScheduleEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleEventId"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("ParentEventId")
                        .HasColumnType("int");

                    b.Property<int>("Participants")
                        .HasColumnType("int");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("TypeEventEnum")
                        .HasColumnType("int");

                    b.HasKey("ScheduleEventId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleEvent", (string)null);

                    b.HasData(
                        new
                        {
                            ScheduleEventId = 1,
                            CreationDate = new DateTime(2023, 2, 6, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "visitar a un colega",
                            Name = "Event Visita",
                            Participants = 2,
                            Place = "Brasil",
                            ScheduleId = 1,
                            TypeEventEnum = 1
                        },
                        new
                        {
                            ScheduleEventId = 2,
                            CreationDate = new DateTime(2023, 2, 6, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "reunion familiar",
                            Name = "Event Familia",
                            Participants = 5,
                            Place = "Bolivia",
                            ScheduleId = 2,
                            TypeEventEnum = 2
                        });
                });

            modelBuilder.Entity("Event_Calendar_WebApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "marco.aguilar@gmail.com",
                            FirstName = "Marco",
                            LastName = "Aguilar",
                            Password = "123",
                            RoleId = 1,
                            UserName = "marco"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "jose@gmail.com",
                            FirstName = "Jose",
                            LastName = "Ramos",
                            Password = "123",
                            RoleId = 2,
                            UserName = "jose"
                        });
                });

            modelBuilder.Entity("Event_Calendar_WebApi.Models.Schedule", b =>
                {
                    b.HasOne("Event_Calendar_WebApi.Models.User", "User")
                        .WithOne("Schedule")
                        .HasForeignKey("Event_Calendar_WebApi.Models.Schedule", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Event_Calendar_WebApi.Models.ScheduleEvent", b =>
                {
                    b.HasOne("Event_Calendar_WebApi.Models.Schedule", "Schedule")
                        .WithMany("ScheduleEvents")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Event_Calendar_WebApi.Models.User", b =>
                {
                    b.HasOne("Event_Calendar_WebApi.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Event_Calendar_WebApi.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Event_Calendar_WebApi.Models.Schedule", b =>
                {
                    b.Navigation("ScheduleEvents");
                });

            modelBuilder.Entity("Event_Calendar_WebApi.Models.User", b =>
                {
                    b.Navigation("Schedule");
                });
#pragma warning restore 612, 618
        }
    }
}
