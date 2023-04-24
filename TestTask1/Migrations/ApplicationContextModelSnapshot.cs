﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestTask1.Data;

#nullable disable

namespace TestTask1.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TestTask1.Models.Domain.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("TestTask1.Models.Domain.Flat", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("FlatName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("HouseID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("HouseID");

                    b.ToTable("Flat", (string)null);
                });

            modelBuilder.Entity("TestTask1.Models.Domain.House", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<byte>("FlatsNumber")
                        .HasColumnType("smallint");

                    b.Property<string>("HouseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StreetID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("StreetID");

                    b.ToTable("House", "FlatsNumber <= 100");
                });

            modelBuilder.Entity("TestTask1.Models.Domain.Owner", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FlatID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("FlatID");

                    b.ToTable("Owner", (string)null);
                });

            modelBuilder.Entity("TestTask1.Models.Domain.Street", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CityID")
                        .HasColumnType("integer");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CityID");

                    b.ToTable("Street", (string)null);
                });

            modelBuilder.Entity("TestTask1.Models.Domain.Flat", b =>
                {
                    b.HasOne("TestTask1.Models.Domain.House", "House")
                        .WithMany("Flats")
                        .HasForeignKey("HouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");
                });

            modelBuilder.Entity("TestTask1.Models.Domain.House", b =>
                {
                    b.HasOne("TestTask1.Models.Domain.Street", "Street")
                        .WithMany("Houses")
                        .HasForeignKey("StreetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Street");
                });

            modelBuilder.Entity("TestTask1.Models.Domain.Owner", b =>
                {
                    b.HasOne("TestTask1.Models.Domain.Flat", "Flat")
                        .WithMany("Owners")
                        .HasForeignKey("FlatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flat");
                });

            modelBuilder.Entity("TestTask1.Models.Domain.Street", b =>
                {
                    b.HasOne("TestTask1.Models.Domain.City", "City")
                        .WithMany("Streets")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("TestTask1.Models.Domain.City", b =>
                {
                    b.Navigation("Streets");
                });

            modelBuilder.Entity("TestTask1.Models.Domain.Flat", b =>
                {
                    b.Navigation("Owners");
                });

            modelBuilder.Entity("TestTask1.Models.Domain.House", b =>
                {
                    b.Navigation("Flats");
                });

            modelBuilder.Entity("TestTask1.Models.Domain.Street", b =>
                {
                    b.Navigation("Houses");
                });
#pragma warning restore 612, 618
        }
    }
}
