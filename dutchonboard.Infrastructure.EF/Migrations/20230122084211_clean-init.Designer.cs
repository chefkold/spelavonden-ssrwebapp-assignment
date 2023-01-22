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
    [Migration("20230122084211_clean-init")]
    partial class cleaninit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BoardGameGameNight", b =>
                {
                    b.Property<int>("GameNightsWhereFeaturedId")
                        .HasColumnType("int");

                    b.Property<int>("GamesId")
                        .HasColumnType("int");

                    b.HasKey("GameNightsWhereFeaturedId", "GamesId");

                    b.HasIndex("GamesId");

                    b.ToTable("BoardGameGameNight");
                });

            modelBuilder.Entity("ConsumptionGameNight", b =>
                {
                    b.Property<int>("ConsumptionsId")
                        .HasColumnType("int");

                    b.Property<int>("GameNightsWhereConsumedId")
                        .HasColumnType("int");

                    b.HasKey("ConsumptionsId", "GameNightsWhereConsumedId");

                    b.HasIndex("GameNightsWhereConsumedId");

                    b.ToTable("ConsumptionGameNight");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.BoardGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageFormat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsForAdults")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BoardGames");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.Consumption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DietRestrictions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Consumption");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.GameNight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsForAdults")
                        .HasColumnType("bit");

                    b.Property<int>("MaxPlayerAmount")
                        .HasColumnType("int");

                    b.Property<int?>("OrganizerId")
                        .HasColumnType("int");

                    b.Property<bool>("Potluck")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("GameNights");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DietRestrictions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Player");
                });

            modelBuilder.Entity("GameNightPlayer", b =>
                {
                    b.Property<int>("JoinedNightsId")
                        .HasColumnType("int");

                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.HasKey("JoinedNightsId", "PlayersId");

                    b.HasIndex("PlayersId");

                    b.ToTable("GameNightPlayer");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.Organizer", b =>
                {
                    b.HasBaseType("dutchonboard.Core.Domain.Models.Player");

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("BoardGameGameNight", b =>
                {
                    b.HasOne("dutchonboard.Core.Domain.Models.GameNight", null)
                        .WithMany()
                        .HasForeignKey("GameNightsWhereFeaturedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dutchonboard.Core.Domain.Models.BoardGame", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConsumptionGameNight", b =>
                {
                    b.HasOne("dutchonboard.Core.Domain.Models.Consumption", null)
                        .WithMany()
                        .HasForeignKey("ConsumptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dutchonboard.Core.Domain.Models.GameNight", null)
                        .WithMany()
                        .HasForeignKey("GameNightsWhereConsumedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.GameNight", b =>
                {
                    b.HasOne("dutchonboard.Core.Domain.Models.Organizer", "Organizer")
                        .WithMany("HostedNights")
                        .HasForeignKey("OrganizerId");

                    b.OwnsOne("dutchonboard.Core.Domain.Models.Address", "Location", b1 =>
                        {
                            b1.Property<int>("GameNightId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

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

                    b.Navigation("Location");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.Player", b =>
                {
                    b.OwnsOne("dutchonboard.Core.Domain.Models.Address", "Address", b1 =>
                        {
                            b1.Property<int>("PlayerId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Number")
                                .HasColumnType("int");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PlayerId");

                            b1.ToTable("Players");

                            b1.WithOwner()
                                .HasForeignKey("PlayerId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("GameNightPlayer", b =>
                {
                    b.HasOne("dutchonboard.Core.Domain.Models.GameNight", null)
                        .WithMany()
                        .HasForeignKey("JoinedNightsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dutchonboard.Core.Domain.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dutchonboard.Core.Domain.Models.Organizer", b =>
                {
                    b.Navigation("HostedNights");
                });
#pragma warning restore 612, 618
        }
    }
}