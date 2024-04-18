﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using T_Plus.ThermalProgram.DatabaseContext;

#nullable disable

namespace T_Plus.ThermalProgram.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240418115552_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("T_Plus.ThermalProgram.Models.ThermalNodeProgram", b =>
                {
                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("RepairCost")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ThermalNodeId")
                        .HasColumnType("uuid");

                    b.Property<string>("ThermalNodeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("ThermalNodes");
                });
#pragma warning restore 612, 618
        }
    }
}
