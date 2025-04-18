﻿// <auto-generated />
using DbPlayground.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbPlayground.Persistence.Migrations
{
    [DbContext(typeof(CalculatorContext))]
    [Migration("20230803042126_CreateSeed")]
    partial class CreateSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DbPlayground.Persistence.Models.Equation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Calculation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Equations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Calculation = "5+5",
                            Result = 10
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
