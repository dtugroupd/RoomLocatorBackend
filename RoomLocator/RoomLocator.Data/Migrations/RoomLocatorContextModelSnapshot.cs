﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoomLocator.Data.Config;

namespace RoomLocator.Data.Migrations
{
    [DbContext(typeof(RoomLocatorContext))]
    partial class RoomLocatorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Index");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("MazeMapSectionId");

                    b.HasKey("Id");

                    b.HasIndex("MazeMapSectionId");

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<bool>("DurationApproximated");

                    b.Property<double>("DurationInHours");

                    b.Property<string>("Speakers");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Feedback", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserId");

                    b.Property<bool?>("Vote");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.MazeMapSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("SurveyId");

                    b.Property<int>("Type");

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

            modelBuilder.Entity("RoomLocator.Domain.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Sensor", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("Provider");

                    b.Property<int>("Type");

                    b.Property<int>("ZLevel");

                    b.HasKey("Id");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.SurveyAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<int>("SurveyId");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyAnswers");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.UserDisclaimer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("HasAcceptedDisclaimer");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDisclaimers");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("RoomLocator.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("ProfileImage");

                    b.Property<string>("StudentId")
                        .IsRequired();

                    b.Property<bool>("UserIsDeleted");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Users");
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

            modelBuilder.Entity("RoomLocator.Domain.Models.Feedback", b =>
                {
                    b.HasOne("RoomLocator.Domain.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId");
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

            modelBuilder.Entity("RoomLocator.Domain.Models.UserDisclaimer", b =>
                {
                    b.HasOne("RoomLocator.Domain.User", "User")
                        .WithOne("UserDisclaimer")
                        .HasForeignKey("RoomLocator.Domain.Models.UserDisclaimer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.UserRole", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomLocator.Domain.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
