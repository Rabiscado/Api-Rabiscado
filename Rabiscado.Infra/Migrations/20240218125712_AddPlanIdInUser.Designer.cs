﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rabiscado.Infra.Contexts;

#nullable disable

namespace Rabiscado.Infra.Migrations
{
    [DbContext(typeof(RabiscadoContext))]
    [Migration("20240218125712_AddPlanIdInUser")]
    partial class AddPlanIdInUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.UseGuidCollation(modelBuilder, "");

            modelBuilder.Entity("Rabiscado.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Disabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Localization")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProfessorEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Video")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Rabiscado.Domain.Entities.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("CoinValue")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("Disabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("Rabiscado.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Coin")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<bool>("Disabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PlanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Rabiscado.Domain.Entities.User", b =>
                {
                    b.HasOne("Rabiscado.Domain.Entities.Plan", "Plan")
                        .WithMany("Users")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Rabiscado.Domain.Entities.Plan", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
