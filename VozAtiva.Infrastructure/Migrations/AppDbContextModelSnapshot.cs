﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VozAtiva.Infrastructure.Context;

#nullable disable

namespace VozAtiva.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VozAtiva.Domain.Entities.Alert", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<int>("AlertTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Disabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("PublicAgentId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AlertTypeId");

                    b.HasIndex("PublicAgentId");

                    b.HasIndex("UserId");

                    b.ToTable("alert");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("AlertId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Disabled")
                        .HasColumnType("boolean");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AlertId");

                    b.ToTable("image");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.PublicAgent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Acronym")
                        .HasColumnType("text");

                    b.Property<int>("AgentTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AgentTypeId");

                    b.ToTable("public_agent");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.Types.AgentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("agent_type");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.Types.AlertType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("alert_type");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("Birthdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<bool>("Disabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FederalCodeClient")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.Alert", b =>
                {
                    b.HasOne("VozAtiva.Domain.Entities.Types.AlertType", "AlertType")
                        .WithMany("Alerts")
                        .HasForeignKey("AlertTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VozAtiva.Domain.Entities.PublicAgent", "PublicAgent")
                        .WithMany("Alerts")
                        .HasForeignKey("PublicAgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VozAtiva.Domain.Entities.User", "User")
                        .WithMany("Alerts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AlertType");

                    b.Navigation("PublicAgent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.Image", b =>
                {
                    b.HasOne("VozAtiva.Domain.Entities.Alert", "Alert")
                        .WithMany("Images")
                        .HasForeignKey("AlertId");

                    b.Navigation("Alert");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.PublicAgent", b =>
                {
                    b.HasOne("VozAtiva.Domain.Entities.Types.AgentType", "AgentType")
                        .WithMany("PublicAgents")
                        .HasForeignKey("AgentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AgentType");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.Alert", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.PublicAgent", b =>
                {
                    b.Navigation("Alerts");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.Types.AgentType", b =>
                {
                    b.Navigation("PublicAgents");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.Types.AlertType", b =>
                {
                    b.Navigation("Alerts");
                });

            modelBuilder.Entity("VozAtiva.Domain.Entities.User", b =>
                {
                    b.Navigation("Alerts");
                });
#pragma warning restore 612, 618
        }
    }
}
