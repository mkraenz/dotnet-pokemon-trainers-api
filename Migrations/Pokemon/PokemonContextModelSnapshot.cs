﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using dotnettest.Pokemon.Data;

#nullable disable

namespace dotnettest.Migrations
{
    [DbContext(typeof(PokemonContext))]
    partial class PokemonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("dotnettest.Pokemon.Models.Pokemon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Nickname")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("integer");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("SpeciesId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("dotnettest.Pokemon.Models.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("SpriteUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("dotnettest.Pokemon.Models.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("dotnettest.Pokemon.Models.Trainer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(998)
                        .HasColumnType("character varying(998)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("PokemonTeam", b =>
                {
                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeamsId")
                        .HasColumnType("uuid");

                    b.HasKey("MembersId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("PokemonTeam");
                });

            modelBuilder.Entity("dotnettest.Pokemon.Models.Pokemon", b =>
                {
                    b.HasOne("dotnettest.Pokemon.Models.Species", "Species")
                        .WithMany("Pokemons")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dotnettest.Pokemon.Models.Trainer", "Trainer")
                        .WithMany("Pokemons")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Species");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("dotnettest.Pokemon.Models.Team", b =>
                {
                    b.HasOne("dotnettest.Pokemon.Models.Trainer", null)
                        .WithMany("Teams")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PokemonTeam", b =>
                {
                    b.HasOne("dotnettest.Pokemon.Models.Pokemon", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dotnettest.Pokemon.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dotnettest.Pokemon.Models.Species", b =>
                {
                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("dotnettest.Pokemon.Models.Trainer", b =>
                {
                    b.Navigation("Pokemons");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
