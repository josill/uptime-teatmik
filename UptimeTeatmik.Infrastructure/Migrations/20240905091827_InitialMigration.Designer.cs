﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UptimeTeatmik.Infrastructure.Persistence;

#nullable disable

namespace UptimeTeatmik.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240905091827_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UptimeTeatmik.Domain.BusinessRegisterEntity", b =>
                {
                    b.Property<Guid>("BusinessRegisterEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BusinessOrLastName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("BusinessOrPersonalCode")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid?>("BusinessRegisterEntityId1")
                        .HasColumnType("uuid");

                    b.Property<string>("BusinessRegisterEntityType")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("BusinessRegisterEntityTypeAbbreviation")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("FormattedJson")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("BusinessRegisterEntityId");

                    b.HasIndex("BusinessRegisterEntityId1");

                    b.ToTable("BusinessRegisterEntities");
                });

            modelBuilder.Entity("UptimeTeatmik.Domain.BusinessRegisterEntity", b =>
                {
                    b.HasOne("UptimeTeatmik.Domain.BusinessRegisterEntity", null)
                        .WithMany("Owners")
                        .HasForeignKey("BusinessRegisterEntityId1");
                });

            modelBuilder.Entity("UptimeTeatmik.Domain.BusinessRegisterEntity", b =>
                {
                    b.Navigation("Owners");
                });
#pragma warning restore 612, 618
        }
    }
}
