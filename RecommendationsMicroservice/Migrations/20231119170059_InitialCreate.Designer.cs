﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecommendationsMicroservice.Persistance;

#nullable disable

namespace RecommendationsMicroservice.Migrations
{
    [DbContext(typeof(RecommendationsContext))]
    [Migration("20231119170059_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RecommendationsMicroservice.Entities.CategoryStatistics", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Category")
                        .HasColumnType("varchar(255)");

                    b.Property<ulong>("Value")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("UserId", "Category");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RecommendationsMicroservice.Entities.UserStatistics", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<ulong>("Interactions")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("UserId");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("RecommendationsMicroservice.Entities.CategoryStatistics", b =>
                {
                    b.HasOne("RecommendationsMicroservice.Entities.UserStatistics", "User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecommendationsMicroservice.Entities.UserStatistics", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}