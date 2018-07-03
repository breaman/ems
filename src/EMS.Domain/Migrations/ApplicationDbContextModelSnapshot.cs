﻿// <auto-generated />
using System;
using EMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EMS.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EMS.Domain.Models.Bracket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventInfoId");

                    b.Property<int?>("FieldId");

                    b.Property<string>("Name");

                    b.Property<int?>("Number");

                    b.Property<string>("Referees");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.HasIndex("FieldId");

                    b.ToTable("Brackets");
                });

            modelBuilder.Entity("EMS.Domain.Models.CompetitionLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisplayOrder");

                    b.Property<int?>("DivisionId");

                    b.Property<int>("EventInfoId");

                    b.Property<int?>("GenderId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.ToTable("CompetitionLevels");
                });

            modelBuilder.Entity("EMS.Domain.Models.CostSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Cost");

                    b.Property<int?>("DivisionId");

                    b.Property<DateTimeOffset?>("EndDate");

                    b.Property<int>("EventInfoId");

                    b.Property<DateTimeOffset?>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.HasIndex("EventInfoId");

                    b.ToTable("CostSchedule");
                });

            modelBuilder.Entity("EMS.Domain.Models.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomKey");

                    b.Property<string>("Description");

                    b.Property<int>("DisplayOrder");

                    b.Property<int>("EventInfoId");

                    b.Property<int?>("GradeLowerLimit");

                    b.Property<int?>("GradeUpperLimit");

                    b.Property<int?>("HeightLowerLimit");

                    b.Property<int?>("HeightUpperLimit");

                    b.Property<bool?>("IsGradeBased");

                    b.Property<bool?>("IsHeightBased");

                    b.Property<int?>("MaximumNumberOfParticipants");

                    b.Property<int?>("MinimumNumberOfParticipants");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("EMS.Domain.Models.EventInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("EventInfo");

                    b.HasData(
                        new { Id = 1, Name = "See3Slam", Year = 2018 },
                        new { Id = 2, Name = "SpikeAndDig", Year = 2018 }
                    );
                });

            modelBuilder.Entity("EMS.Domain.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventInfoId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("EMS.Domain.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BracketId");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<int>("EventInfoId");

                    b.Property<int?>("FieldId");

                    b.Property<int?>("GameNumber");

                    b.Property<DateTimeOffset>("GameTime");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTimeOffset?>("ModifiedOn");

                    b.Property<int?>("Team1Id");

                    b.Property<int?>("Team1Score");

                    b.Property<int?>("Team2Id");

                    b.Property<int?>("Team2Score");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.HasIndex("FieldId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("EMS.Domain.Models.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisplayOrder");

                    b.Property<int?>("DivisionId");

                    b.Property<int>("EventInfoId");

                    b.Property<int?>("MinimumNumberOfFemaleParticipants");

                    b.Property<int?>("MinimumNumberOfMaleParticipants");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("EMS.Domain.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<int>("EventInfoId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTimeOffset?>("ModifiedOn");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.HasIndex("TeamId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("EMS.Domain.Models.ParticipantInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventInfoId");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.ToTable("ParticipantInfo");
                });

            modelBuilder.Entity("EMS.Domain.Models.PromotionalCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("DiscountPercentage");

                    b.Property<int>("EventInfoId");

                    b.Property<int?>("FlatRateCost");

                    b.Property<bool?>("IsPlayerCode");

                    b.Property<string>("Name");

                    b.Property<int?>("NumberOfCodes");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.ToTable("PromotionalCodes");
                });

            modelBuilder.Entity("EMS.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("EMS.Domain.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BracketId");

                    b.Property<int?>("CompetitionLevelId");

                    b.Property<int?>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<DateTimeOffset?>("DeletedOn");

                    b.Property<int?>("DivisionId");

                    b.Property<int>("EventInfoId");

                    b.Property<int?>("GenderId");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("ManagerId");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTimeOffset?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("PaymentTransactionId");

                    b.Property<int?>("PromotionalCodeId");

                    b.HasKey("Id");

                    b.HasIndex("BracketId");

                    b.HasIndex("CompetitionLevelId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("EventInfoId");

                    b.HasIndex("GenderId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("PromotionalCodeId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("EMS.Domain.Models.TeamInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventInfoId");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.ToTable("TeamInfo");
                });

            modelBuilder.Entity("EMS.Domain.Models.TournamentParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventInfoId");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.ToTable("TournamentParameters");
                });

            modelBuilder.Entity("EMS.Domain.Models.TransactionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<int>("EventInfoId");

                    b.Property<int>("ManagerId");

                    b.Property<string>("PaymentStatus");

                    b.Property<DateTimeOffset>("TransactionDate");

                    b.Property<string>("TransactionId");

                    b.HasKey("Id");

                    b.HasIndex("EventInfoId");

                    b.ToTable("TransactionHistory");
                });

            modelBuilder.Entity("EMS.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EMS.Domain.Models.Bracket", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EMS.Domain.Models.Field", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId");
                });

            modelBuilder.Entity("EMS.Domain.Models.CompetitionLevel", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Models.CostSchedule", b =>
                {
                    b.HasOne("EMS.Domain.Models.Division", "Division")
                        .WithMany("Cost")
                        .HasForeignKey("DivisionId");

                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Models.Division", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Models.Field", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Models.Game", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EMS.Domain.Models.Field", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId");
                });

            modelBuilder.Entity("EMS.Domain.Models.Gender", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Models.Participant", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EMS.Domain.Models.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("EMS.Domain.Models.ParticipantInfo", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Models.PromotionalCode", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Models.Team", b =>
                {
                    b.HasOne("EMS.Domain.Models.Bracket", "Bracket")
                        .WithMany()
                        .HasForeignKey("BracketId");

                    b.HasOne("EMS.Domain.Models.CompetitionLevel", "CompetitionLevel")
                        .WithMany()
                        .HasForeignKey("CompetitionLevelId");

                    b.HasOne("EMS.Domain.Models.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId");

                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EMS.Domain.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.HasOne("EMS.Domain.Models.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EMS.Domain.Models.PromotionalCode", "PromotionalCode")
                        .WithMany()
                        .HasForeignKey("PromotionalCodeId");
                });

            modelBuilder.Entity("EMS.Domain.Models.TeamInfo", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Models.TournamentParameter", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EMS.Domain.Models.TransactionHistory", b =>
                {
                    b.HasOne("EMS.Domain.Models.EventInfo", "EventInfo")
                        .WithMany()
                        .HasForeignKey("EventInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("EMS.Domain.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("EMS.Domain.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("EMS.Domain.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("EMS.Domain.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EMS.Domain.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("EMS.Domain.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
