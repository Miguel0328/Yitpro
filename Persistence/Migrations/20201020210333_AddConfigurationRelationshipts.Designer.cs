﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201020210333_AddConfigurationRelationshipts")]
    partial class AddConfigurationRelationshipts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Persistence.Models.MenuModel", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Controller")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<long>("Order")
                        .HasColumnType("bigint");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId")
                        .IsUnique()
                        .HasFilter("[ParentId] IS NOT NULL");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("Persistence.Models.RoleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("Protected")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Persistence.Models.RolePermissionsModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Delete")
                        .HasColumnType("bit");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<bool>("Modify")
                        .HasColumnType("bit");

                    b.Property<bool>("Print")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("Save")
                        .HasColumnType("bit");

                    b.Property<bool>("Watch")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Persistence.Models.UserModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("AdmissionDate")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("EmployeeNumber")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

                    b.Property<long?>("ManagerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("PasswordLastUpdate")
                        .HasColumnType("smalldatetime");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("smalldatetime");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ManagerId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Persistence.Models.MenuModel", b =>
                {
                    b.HasOne("Persistence.Models.MenuModel", "Parent")
                        .WithOne()
                        .HasForeignKey("Persistence.Models.MenuModel", "ParentId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Persistence.Models.RoleModel", b =>
                {
                    b.HasOne("Persistence.Models.UserModel", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Persistence.Models.UserModel", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Persistence.Models.RolePermissionsModel", b =>
                {
                    b.HasOne("Persistence.Models.MenuModel", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.RoleModel", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Persistence.Models.UserModel", b =>
                {
                    b.HasOne("Persistence.Models.UserModel", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Persistence.Models.UserModel", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Persistence.Models.RoleModel", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.UserModel", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.NoAction);
                });
#pragma warning restore 612, 618
        }
    }
}
