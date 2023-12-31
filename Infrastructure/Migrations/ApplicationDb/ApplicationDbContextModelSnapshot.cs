﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("BringSnacks")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DateAndTime")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int?>("FoodAndDrinkOptionsId")
                        .HasColumnType("int");

                    b.Property<bool>("Is18Plus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOrganizerOverride18Plus")
                        .HasColumnType("bit");

                    b.Property<int?>("MaxPlayers")
                        .IsRequired()
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

                    b.HasIndex("SelectedBoardGameId");

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

                    b.Property<DateTime>("JoinDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoardGameNightId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Core.Domain.Entities.Snacks", b =>
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

                    b.ToTable("Snacks");
                });

            modelBuilder.Entity("Core.Domain.Entities.BoardGame", b =>
                {
                    b.HasOne("Core.Domain.Entities.BoardGameNight", null)
                        .WithMany("Games")
                        .HasForeignKey("BoardGameNightId");

                    b.OwnsOne("Core.Domain.Entities.GameType", "GameType", b1 =>
                        {
                            b1.Property<int>("BoardGameId")
                                .HasColumnType("int");

                            b1.Property<string>("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BoardGameId");

                            b1.ToTable("BoardGames");

                            b1.WithOwner()
                                .HasForeignKey("BoardGameId");
                        });

                    b.Navigation("GameType")
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.Entities.BoardGameNight", b =>
                {
                    b.HasOne("Core.Domain.Entities.FoodAndDrinkOption", "FoodAndDrinkOptions")
                        .WithMany()
                        .HasForeignKey("FoodAndDrinkOptionsId");

                    b.HasOne("Core.Domain.Entities.BoardGame", "SelectedBoardGame")
                        .WithMany()
                        .HasForeignKey("SelectedBoardGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodAndDrinkOptions");

                    b.Navigation("SelectedBoardGame");
                });

            modelBuilder.Entity("Core.Domain.Entities.Player", b =>
                {
                    b.HasOne("Core.Domain.Entities.BoardGameNight", null)
                        .WithMany("Players")
                        .HasForeignKey("BoardGameNightId");
                });

            modelBuilder.Entity("Core.Domain.Entities.Snacks", b =>
                {
                    b.HasOne("Core.Domain.Entities.BoardGameNight", null)
                        .WithMany("Snacks")
                        .HasForeignKey("BoardGameNightId");
                });

            modelBuilder.Entity("Core.Domain.Entities.BoardGameNight", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("Players");

                    b.Navigation("Snacks");
                });
#pragma warning restore 612, 618
        }
    }
}
