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
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Index");

                    b.Property<double>("Latitude");

                    b.Property<string>("LocationId");

                    b.Property<double>("Longitude");

                    b.Property<string>("SectionId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("SectionId");

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

                    b.Property<double>("Latitude");

                    b.Property<string>("LocationId");

                    b.Property<double>("Longitude");

                    b.Property<string>("Speakers");

                    b.Property<string>("Title");

                    b.Property<int>("ZLevel");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Feedback", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LocationId");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserId");

                    b.Property<bool?>("Vote");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Location", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<double>("Zoom");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Question", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SurveyId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.QuestionAnswer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("QuestionId");

                    b.Property<int>("Score");

                    b.Property<string>("SurveyAnswerId");

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

            modelBuilder.Entity("RoomLocator.Domain.Models.Section", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LocationId");

                    b.Property<string>("SurveyId");

                    b.Property<int>("Type");

                    b.Property<int>("ZLevel");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("SurveyId");

                    b.ToTable("Sections");
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
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.SurveyAnswer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<string>("SurveyId");

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
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LocationId");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId", "LocationId")
                        .IsUnique()
                        .HasFilter("[LocationId] IS NOT NULL");

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
                    b.HasOne("RoomLocator.Domain.Models.Location", "Location")
                        .WithMany("Coordinates")
                        .HasForeignKey("LocationId");

                    b.HasOne("RoomLocator.Domain.Models.Section", "MazeMapSection")
                        .WithMany("Coordinates")
                        .HasForeignKey("SectionId");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Event", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Location", "Location")
                        .WithMany("Events")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Feedback", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Location", "Location")
                        .WithMany("Feedbacks")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomLocator.Domain.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Question", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.QuestionAnswer", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Question", "Question")
                        .WithMany("QuestionAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RoomLocator.Domain.Models.SurveyAnswer", "SurveyAnswer")
                        .WithMany("QuestionAnswers")
                        .HasForeignKey("SurveyAnswerId");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.Section", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Location", "Location")
                        .WithMany("Sections")
                        .HasForeignKey("LocationId");

                    b.HasOne("RoomLocator.Domain.Models.Survey", "Survey")
                        .WithMany("Sections")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("RoomLocator.Domain.Models.SurveyAnswer", b =>
                {
                    b.HasOne("RoomLocator.Domain.Models.Survey", "Survey")
                        .WithMany("SurveyAnswers")
                        .HasForeignKey("SurveyId");
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
                    b.HasOne("RoomLocator.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

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
