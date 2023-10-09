﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231006084755_IntialCreate")]
    partial class IntialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Domain.Entities.BoardGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BoardGameNightId")
                        .HasColumnType("int");

                    b.Property<int?>("BoardGameNightId1")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<bool>("Is18Plus")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoardGameNightId");

                    b.HasIndex("BoardGameNightId1");

                    b.ToTable("BoardGames");
                });

            modelBuilder.Entity("Core.Domain.Entities.BoardGameNight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateAndTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("FoodAndDrinkOptionsId")
                        .HasColumnType("int");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<string>("OrganizerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SelectedBoardGameId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodAndDrinkOptionsId");

                    b.ToTable("BoardGameNights");
                });

            modelBuilder.Entity("Core.Domain.Entities.FoodAndDrinkOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("LactoseFree")
                        .HasColumnType("bit");

                    b.Property<bool>("NonAlcoholic")
                        .HasColumnType("bit");

                    b.Property<bool>("NutFree")
                        .HasColumnType("bit");

                    b.Property<bool>("Vegetarian")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("FoodAndDrinkOption");
                });

            modelBuilder.Entity("Core.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BoardGameNightId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoardGameNightId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Core.Domain.Entities.BoardGame", b =>
                {
                    b.HasOne("Core.Domain.Entities.BoardGameNight", null)
                        .WithMany("AvailableBoardGames")
                        .HasForeignKey("BoardGameNightId");

                    b.HasOne("Core.Domain.Entities.BoardGameNight", null)
                        .WithMany("Games")
                        .HasForeignKey("BoardGameNightId1");

                    b.OwnsOne("Core.Domain.Entities.GameType", "GameType", b1 =>
                        {
                            b1.Property<int>("BoardGameId")
                                .HasColumnType("int");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BoardGameId");

                            b1.ToTable("BoardGames");

                            b1.WithOwner()
                                .HasForeignKey("BoardGameId");
                        });

                    b.Navigation("GameType");
                });

            modelBuilder.Entity("Core.Domain.Entities.BoardGameNight", b =>
                {
                    b.HasOne("Core.Domain.Entities.FoodAndDrinkOption", "FoodAndDrinkOptions")
                        .WithMany()
                        .HasForeignKey("FoodAndDrinkOptionsId");

                    b.Navigation("FoodAndDrinkOptions");
                });

            modelBuilder.Entity("Core.Domain.Entities.Player", b =>
                {
                    b.HasOne("Core.Domain.Entities.BoardGameNight", null)
                        .WithMany("Players")
                        .HasForeignKey("BoardGameNightId");
                });

            modelBuilder.Entity("Core.Domain.Entities.BoardGameNight", b =>
                {
                    b.Navigation("AvailableBoardGames");

                    b.Navigation("Games");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}