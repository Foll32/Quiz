﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quiz.QuestionStorage.Db;

#nullable disable

namespace Quiz.QuestionStorage.Db.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Quiz.CommonModels.Answers.AnswerDefinition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("NotesForHost")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("NotesForPlayers")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AnswerDefinition");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Quiz.CommonModels.Formulations.QuestionFormulation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("QuestionFormulation");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Quiz.CommonModels.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("AnswerDefinitionId")
                        .HasColumnType("bigint");

                    b.Property<long>("QuestionFormulationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AnswerDefinitionId");

                    b.HasIndex("QuestionFormulationId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Quiz.CommonModels.Answers.FreeTextAnswerDefinition", b =>
                {
                    b.HasBaseType("Quiz.CommonModels.Answers.AnswerDefinition");

                    b.Property<string>("AdditionalAnswers")
                        .HasMaxLength(1500)
                        .HasColumnType("varchar(1500)");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Quiz.CommonModels.Formulations.TextOnlyFormulation", b =>
                {
                    b.HasBaseType("Quiz.CommonModels.Formulations.QuestionFormulation");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Quiz.CommonModels.Question", b =>
                {
                    b.HasOne("Quiz.CommonModels.Answers.AnswerDefinition", "AnswerDefinition")
                        .WithMany()
                        .HasForeignKey("AnswerDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quiz.CommonModels.Formulations.QuestionFormulation", "QuestionFormulation")
                        .WithMany()
                        .HasForeignKey("QuestionFormulationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnswerDefinition");

                    b.Navigation("QuestionFormulation");
                });
#pragma warning restore 612, 618
        }
    }
}