﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dutchonboard.Infrastructure.EF.Data;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    [DbContext(typeof(DutchOnBoardDbContext))]
    [Migration("20221019210702_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.BoardGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GameNightId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameNightId");

                    b.ToTable("BoardGame");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.GameNight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AdultOnly")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HostId")
                        .HasColumnType("int");

                    b.Property<int>("MaxPlayerAmount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.ToTable("GameNights");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameNightId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameNightId");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.Organizer", b =>
                {
                    b.HasBaseType("dutchonboard.Core.Domain.Models.Person");

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.BoardGame", b =>
                {
                    b.HasOne("dutchonboard.Core.Domain.Models.GameNight", null)
                        .WithMany("Games")
                        .HasForeignKey("GameNightId");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.GameNight", b =>
                {
                    b.HasOne("dutchonboard.Core.Domain.Models.Organizer", "Host")
                        .WithMany()
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("dutchonboard.Core.Domain.Models.Address", "Location", b1 =>
                        {
                            b1.Property<int>("GameNightId")
                                .HasColumnType("int");

                            b1.Property<int>("Number")
                                .HasColumnType("int");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("GameNightId");

                            b1.ToTable("GameNights");

                            b1.WithOwner()
                                .HasForeignKey("GameNightId");
                        });

                    b.Navigation("Host");

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.Person", b =>
                {
                    b.HasOne("dutchonboard.Core.Domain.Models.GameNight", null)
                        .WithMany("Players")
                        .HasForeignKey("GameNightId");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.GameNight", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
