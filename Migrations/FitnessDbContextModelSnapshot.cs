﻿// <auto-generated />
using System;
using HealthAndFitnessTracker.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthAndFitnessTracker.Migrations
{
    [DbContext(typeof(FitnessDbContext))]
    partial class FitnessDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("HealthAndFitnessTracker.Classes.BodyMeasurements", b =>
                {
                    b.Property<int>("BodymeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("BodyfatPercentage")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("BodymeasurementDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("BodymeasurementId");

                    b.HasIndex("PersonId");

                    b.ToTable("BodyMeasurements");
                });

            modelBuilder.Entity("HealthAndFitnessTracker.Classes.FoodLog", b =>
                {
                    b.Property<int>("FoodlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("FoodCalories")
                        .HasColumnType("REAL");

                    b.Property<string>("FoodItem")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FoodlogDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MealType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FoodlogId");

                    b.HasIndex("PersonId");

                    b.ToTable("FoodLogs");
                });

            modelBuilder.Entity("HealthAndFitnessTracker.Classes.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonAge")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("HealthAndFitnessTracker.Classes.Workout", b =>
                {
                    b.Property<int>("WorkoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("CaloriesBurned")
                        .HasColumnType("REAL");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("WorkoutDate")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("WorkoutFinished")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("WorkoutStart")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkoutType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("WorkoutId");

                    b.HasIndex("PersonId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("HealthAndFitnessTracker.Classes.BodyMeasurements", b =>
                {
                    b.HasOne("HealthAndFitnessTracker.Classes.Person", null)
                        .WithMany("BodyMeasurements")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HealthAndFitnessTracker.Classes.FoodLog", b =>
                {
                    b.HasOne("HealthAndFitnessTracker.Classes.Person", null)
                        .WithMany("FoodLogs")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HealthAndFitnessTracker.Classes.Workout", b =>
                {
                    b.HasOne("HealthAndFitnessTracker.Classes.Person", null)
                        .WithMany("Workouts")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HealthAndFitnessTracker.Classes.Person", b =>
                {
                    b.Navigation("BodyMeasurements");

                    b.Navigation("FoodLogs");

                    b.Navigation("Workouts");
                });
#pragma warning restore 612, 618
        }
    }
}
