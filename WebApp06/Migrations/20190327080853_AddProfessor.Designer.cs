﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApp06.Data;
using WebApp06.Models.Test;

namespace WebApp06.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190327080853_AddProfessor")]
    partial class AddProfessor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			modelBuilder.Entity("WebApp06.Models.ApplicationUser", b =>
			{
				b.Property<string>("Id")
						.ValueGeneratedOnAdd();

				b.Property<int>("AccessFailedCount");

				b.Property<string>("ConcurrencyStamp")
						.IsConcurrencyToken();

				b.Property<string>("Email")
						.HasMaxLength(256);

				b.Property<bool>("EmailConfirmed");

				b.Property<bool>("LockoutEnabled");

				b.Property<DateTimeOffset?>("LockoutEnd");

				b.Property<string>("NormalizedEmail")
						.HasMaxLength(256);

				b.Property<string>("NormalizedUserName")
						.HasMaxLength(256);

				b.Property<string>("PasswordHash");

				b.Property<string>("PhoneNumber");

				b.Property<bool>("PhoneNumberConfirmed");

				b.Property<string>("SecurityStamp");

				b.Property<bool>("TwoFactorEnabled");

				b.Property<string>("UserName")
						.HasMaxLength(256);

				b.HasKey("Id");

				b.HasIndex("NormalizedEmail")
						.HasName("EmailIndex");

				b.HasIndex("NormalizedUserName")
						.IsUnique()
						.HasName("UserNameIndex")
						.HasFilter("[NormalizedUserName] IS NOT NULL");

				b.ToTable("AspNetUsers");
			});


			modelBuilder.Entity("WebApp06.Models.Test.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WebApp06.Models.Test.Professor", b =>
                {
                    b.Property<int>("SubjectId");

                    b.Property<string>("UserId");

                    b.HasKey("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("WebApp06.Models.Test.SubjectRole", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("WebApp06.Models.Test.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
