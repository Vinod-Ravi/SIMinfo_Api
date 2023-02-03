﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SIMinfo.API.DataAccessLayer;

#nullable disable

namespace SIMinfo.API.Migrations
{
    [DbContext(typeof(SimInfoDbContext))]
    partial class SimInfoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SIMinfo.API.Models.MobileCountryCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CodeName")
                        .HasColumnType("text");

                    b.Property<string>("CountryCode")
                        .HasColumnType("text");

                    b.Property<string>("CountryName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MobileCountryCode");
                });

            modelBuilder.Entity("SIMinfo.API.Models.SimInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdviceOfCharge")
                        .HasColumnType("text");

                    b.Property<string>("AuthenticationKey")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("text");

                    b.Property<string>("IntegratedCircuitCardId")
                        .HasColumnType("text");

                    b.Property<string>("LocalAreaIdentity")
                        .HasColumnType("text");

                    b.Property<string>("MobileCountryCode")
                        .HasColumnType("text");

                    b.Property<string>("ServiceProviderName")
                        .HasColumnType("text");

                    b.Property<string>("ValueAddedServices")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SimInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
