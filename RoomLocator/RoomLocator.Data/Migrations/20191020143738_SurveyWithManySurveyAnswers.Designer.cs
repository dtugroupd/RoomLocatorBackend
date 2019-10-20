﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoomLocator.Data.Config;

namespace RoomLocator.Data.Migrations
{
    [DbContext(typeof(RoomLocatorContext))]
    [Migration("20191020143738_SurveyWithManySurveyAnswers")]
    partial class SurveyWithManySurveyAnswers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RoomLocator.Domain.Models.Coordinates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("MazeMapSectionId");

                    b.HasKey("Id");

                    b.HasIndex("MazeMapSectionId");

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.MazeMapSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("SurveyId");

                    b.Property<int>("ZLevel");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("MazeMapSections");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SurveyId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.QuestionAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("QuestionId");

                    b.Property<int>("Score");

                    b.Property<int>("SurveyAnswerId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SurveyAnswerId");

                    b.ToTable("QuestionAnswers");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.SurveyAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SurveyId");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyAnswers");
                });

            modelBuilder.Entity("RoomLocator.Domain.Value", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Coordinates", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.MazeMapSection", "MazeMapSection")
                        .WithMany("Coordinates")
                        .HasForeignKey("MazeMapSectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.MazeMapSection", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Survey", "Survey")
                        .WithMany("MazeMapSections")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Question", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.QuestionAnswer", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Question", "Question")
                        .WithMany("QuestionAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RoomLocator.Domain.Models.SurveyAnswer", "SurveyAnswer")
                        .WithMany("QuestionAnswers")
                        .HasForeignKey("SurveyAnswerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.SurveyAnswer", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Survey", "Survey")
                        .WithMany("SurveyAnswers")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
