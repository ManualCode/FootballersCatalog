﻿// <auto-generated />
using System;
using FootballersCatalog.DataAcces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FootballersCatalog.DataAcces.Migrations
{
    [DbContext(typeof(FootballersCatalogDbContext))]
    [Migration("20250123051552_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FootballersCatalog.DataAcces.Entities.CountryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CountryName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ac467b45-b198-4fa6-907e-f54885506297"),
                            CountryName = "Россия"
                        },
                        new
                        {
                            Id = new Guid("6fb36ed1-9668-46ad-b3dd-518b682043a8"),
                            CountryName = "США"
                        },
                        new
                        {
                            Id = new Guid("e7a66b27-01e6-4a31-9c1a-7416ae292a7a"),
                            CountryName = "Италия"
                        });
                });

            modelBuilder.Entity("FootballersCatalog.DataAcces.Entities.FootballerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("TeamId");

                    b.ToTable("Footballers");
                });

            modelBuilder.Entity("FootballersCatalog.DataAcces.Entities.TeamEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("TeamName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FootballersCatalog.DataAcces.Entities.FootballerEntity", b =>
                {
                    b.HasOne("FootballersCatalog.DataAcces.Entities.CountryEntity", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FootballersCatalog.DataAcces.Entities.TeamEntity", "Team")
                        .WithMany("Footballers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FootballersCatalog.DataAcces.Entities.TeamEntity", b =>
                {
                    b.Navigation("Footballers");
                });
#pragma warning restore 612, 618
        }
    }
}
