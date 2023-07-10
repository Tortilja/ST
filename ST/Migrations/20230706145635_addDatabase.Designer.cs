﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ST.Data;

#nullable disable

namespace ST.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230706145635_addDatabase")]
    partial class addDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ST.Models.About", b =>
                {
                    b.Property<int>("About_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("About_ID"));

                    b.Property<string>("About_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("About_ID");

                    b.ToTable("About");
                });

            modelBuilder.Entity("ST.Models.CompetitionRegistration", b =>
                {
                    b.Property<int>("Competition_Registration_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Competition_Registration_ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone_Number")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Competition_Registration_ID");

                    b.ToTable("CompetitionRegistration");
                });

            modelBuilder.Entity("ST.Models.Event", b =>
                {
                    b.Property<int>("Event_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Event_ID"));

                    b.Property<string>("Event_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Event_ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Event_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Event_ID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("ST.Models.ImageGallery", b =>
                {
                    b.Property<int>("Image_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Image_ID"));

                    b.Property<int>("Event_FK_ID")
                        .HasColumnType("int");

                    b.Property<string>("Image_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Image_ID");

                    b.HasIndex("Event_FK_ID");

                    b.ToTable("ImageGallery");
                });

            modelBuilder.Entity("ST.Models.Sponsor", b =>
                {
                    b.Property<int>("Sponsor_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sponsor_ID"));

                    b.Property<string>("Sponsor_Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sponsor_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sponsor_ID");

                    b.ToTable("Sponsor");
                });

            modelBuilder.Entity("ST.Models.ImageGallery", b =>
                {
                    b.HasOne("ST.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("Event_FK_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });
#pragma warning restore 612, 618
        }
    }
}
