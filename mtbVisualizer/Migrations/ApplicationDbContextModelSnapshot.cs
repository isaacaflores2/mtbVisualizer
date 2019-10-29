﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MtbVisualizer.Data;

namespace mtbVisualizer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IO.Swagger.Model.PolylineMap", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Polyline");

                    b.Property<string>("SummaryPolyline");

                    b.HasKey("Id");

                    b.ToTable("PolylineMap");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MtbVisualizer.Models.Activities.StravaUser", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<DateTime>("LastDownload");

                    b.HasKey("UserId");

                    b.ToTable("StravaUsers");
                });

            modelBuilder.Entity("MtbVisualizer.Models.Activities.VisualActivity", b =>
                {
                    b.Property<long?>("ActivityId");

                    b.Property<float?>("EndLat");

                    b.Property<float?>("EndLong");

                    b.Property<float?>("StartLat");

                    b.Property<float?>("StartLong");

                    b.Property<string>("TrailName");

                    b.Property<int?>("UserId");

                    b.HasKey("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("VisualActivities");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MtbVisualizer.Models.Activities.VisualActivity", b =>
                {
                    b.HasOne("MtbVisualizer.Models.Activities.StravaUser")
                        .WithMany("VisualActivities")
                        .HasForeignKey("UserId");

                    b.OwnsOne("IO.Swagger.Model.SummaryActivity", "Summary", b1 =>
                        {
                            b1.Property<long?>("VisualActivityActivityId");

                            b1.Property<int?>("AchievementCount");

                            b1.Property<int?>("AthleteCount");

                            b1.Property<float?>("AverageSpeed");

                            b1.Property<float?>("AverageWatts");

                            b1.Property<int?>("CommentCount");

                            b1.Property<bool?>("Commute");

                            b1.Property<bool?>("DeviceWatts");

                            b1.Property<float?>("Distance");

                            b1.Property<int?>("ElapsedTime");

                            b1.Property<float?>("ElevHigh");

                            b1.Property<float?>("ElevLow");

                            b1.Property<string>("ExternalId");

                            b1.Property<bool?>("Flagged");

                            b1.Property<string>("GearId");

                            b1.Property<bool?>("HasKudoed");

                            b1.Property<long?>("Id");

                            b1.Property<float?>("Kilojoules");

                            b1.Property<int?>("KudosCount");

                            b1.Property<bool?>("Manual");

                            b1.Property<string>("MapId");

                            b1.Property<float?>("MaxSpeed");

                            b1.Property<int?>("MaxWatts");

                            b1.Property<int?>("MovingTime");

                            b1.Property<string>("Name");

                            b1.Property<int?>("PhotoCount");

                            b1.Property<bool?>("Private");

                            b1.Property<DateTime?>("StartDate");

                            b1.Property<DateTime?>("StartDateLocal");

                            b1.Property<string>("Timezone");

                            b1.Property<float?>("TotalElevationGain");

                            b1.Property<int?>("TotalPhotoCount");

                            b1.Property<bool?>("Trainer");

                            b1.Property<int>("Type");

                            b1.Property<long?>("UploadId");

                            b1.Property<string>("UploadIdStr");

                            b1.Property<int?>("WeightedAverageWatts");

                            b1.Property<int?>("WorkoutType");

                            b1.HasKey("VisualActivityActivityId");

                            b1.HasIndex("MapId");

                            b1.ToTable("VisualActivities");

                            b1.HasOne("IO.Swagger.Model.PolylineMap", "Map")
                                .WithMany()
                                .HasForeignKey("MapId");

                            b1.HasOne("MtbVisualizer.Models.Activities.VisualActivity")
                                .WithOne("Summary")
                                .HasForeignKey("IO.Swagger.Model.SummaryActivity", "VisualActivityActivityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
