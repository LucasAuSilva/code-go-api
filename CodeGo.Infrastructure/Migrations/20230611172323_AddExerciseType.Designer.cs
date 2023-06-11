﻿// <auto-generated />
using System;
using CodeGo.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodeGo.Infrastructure.Migrations
{
    [DbContext(typeof(CodeGoDbContext))]
    [Migration("20230611172323_AddExerciseType")]
    partial class AddExerciseType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CodeGo.Domain.CourseAggregateRoot.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("CourseIcon")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Language")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("courses", (string)null);
                });

            modelBuilder.Entity("CodeGo.Domain.ExerciseAggregateRoot.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("BaseCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("exercises", (string)null);
                });

            modelBuilder.Entity("CodeGo.Domain.QuestionAggregateRoot.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("questions", (string)null);
                });

            modelBuilder.Entity("CodeGo.Domain.CourseAggregateRoot.Course", b =>
                {
                    b.OwnsMany("CodeGo.Domain.CourseAggregateRoot.Entities.Section", "Sections", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("SectionId");

                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("character varying(150)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("Id", "CourseId");

                            b1.HasIndex("CourseId");

                            b1.ToTable("sections", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CourseId");

                            b1.OwnsMany("CodeGo.Domain.CourseAggregateRoot.Entities.Module", "Modules", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uuid")
                                        .HasColumnName("ModuleId");

                                    b2.Property<Guid>("SectionId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("CourseId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<int>("TotalLessons")
                                        .HasColumnType("integer");

                                    b2.Property<int>("Type")
                                        .HasColumnType("integer");

                                    b2.HasKey("Id", "SectionId", "CourseId");

                                    b2.HasIndex("SectionId", "CourseId");

                                    b2.ToTable("modules", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("SectionId", "CourseId");

                                    b2.OwnsOne("CodeGo.Domain.Common.ValueObjects.Difficulty", "Difficulty", b3 =>
                                        {
                                            b3.Property<Guid>("ModuleId")
                                                .HasColumnType("uuid");

                                            b3.Property<Guid>("ModuleSectionId")
                                                .HasColumnType("uuid");

                                            b3.Property<Guid>("ModuleCourseId")
                                                .HasColumnType("uuid");

                                            b3.Property<int>("Value")
                                                .HasColumnType("integer");

                                            b3.HasKey("ModuleId", "ModuleSectionId", "ModuleCourseId");

                                            b3.ToTable("modules");

                                            b3.WithOwner()
                                                .HasForeignKey("ModuleId", "ModuleSectionId", "ModuleCourseId");
                                        });

                                    b2.Navigation("Difficulty")
                                        .IsRequired();
                                });

                            b1.Navigation("Modules");
                        });

                    b.OwnsMany("CodeGo.Domain.ExerciseAggregateRoot.ValueObjects.ExerciseId", "ExerciseIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("ExerciseId");

                            b1.HasKey("Id");

                            b1.HasIndex("CourseId");

                            b1.ToTable("courseExerciseIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.OwnsMany("CodeGo.Domain.QuestionAggregateRoot.ValueObjects.QuestionId", "QuestionIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("QuestionId");

                            b1.HasKey("Id");

                            b1.HasIndex("CourseId");

                            b1.ToTable("courseQuestionIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.Navigation("ExerciseIds");

                    b.Navigation("QuestionIds");

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("CodeGo.Domain.ExerciseAggregateRoot.Exercise", b =>
                {
                    b.OwnsMany("CodeGo.Domain.ExerciseAggregateRoot.Entities.TestCase", "TestCases", b1 =>
                        {
                            b1.Property<Guid>("ExerciseId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("TestCaseId");

                            b1.Property<string>("Result")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.HasKey("ExerciseId", "Id");

                            b1.ToTable("testCases", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ExerciseId");
                        });

                    b.OwnsOne("CodeGo.Domain.Common.ValueObjects.Difficulty", "Difficulty", b1 =>
                        {
                            b1.Property<Guid>("ExerciseId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer");

                            b1.HasKey("ExerciseId");

                            b1.ToTable("exercises");

                            b1.WithOwner()
                                .HasForeignKey("ExerciseId");
                        });

                    b.Navigation("Difficulty")
                        .IsRequired();

                    b.Navigation("TestCases");
                });

            modelBuilder.Entity("CodeGo.Domain.QuestionAggregateRoot.Question", b =>
                {
                    b.OwnsMany("CodeGo.Domain.QuestionAggregateRoot.Entity.Alternative", "Alternatives", b1 =>
                        {
                            b1.Property<Guid>("QuestionId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("AlternativeId");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<bool>("IsCorrect")
                                .HasColumnType("boolean");

                            b1.HasKey("QuestionId", "Id");

                            b1.ToTable("alternatives", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("QuestionId");
                        });

                    b.OwnsOne("CodeGo.Domain.Common.ValueObjects.Difficulty", "Difficulty", b1 =>
                        {
                            b1.Property<Guid>("QuestionId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer");

                            b1.HasKey("QuestionId");

                            b1.ToTable("questions");

                            b1.WithOwner()
                                .HasForeignKey("QuestionId");
                        });

                    b.Navigation("Alternatives");

                    b.Navigation("Difficulty")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}