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
    [Migration("20240909121055_OptionalRoles")]
    partial class OptionalRoles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UptimeTeatmik.Domain.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BusinessOrLastName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("BusinessOrPersonalCode")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EntityType")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("EntityTypeAbbreviation")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("FormattedJson")
                        .HasColumnType("text");

                    b.Property<string>("UniqueCode")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(448)
                        .HasColumnType("character varying(448)")
                        .HasComputedColumnSql("COALESCE(\"FirstName\", '') || COALESCE(\"BusinessOrLastName\", '') || \"BusinessOrPersonalCode\"", true);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("UptimeTeatmik.Domain.EntityOwner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("RoleInEntity")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("RoleInEntityAbbreviation")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnedId");

                    b.HasIndex("OwnerId");

                    b.ToTable("EntityOwners");
                });

            modelBuilder.Entity("UptimeTeatmik.Domain.EntityOwner", b =>
                {
                    b.HasOne("UptimeTeatmik.Domain.Entity", "Owned")
                        .WithMany()
                        .HasForeignKey("OwnedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UptimeTeatmik.Domain.Entity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owned");

                    b.Navigation("Owner");
                });
#pragma warning restore 612, 618
        }
    }
}
