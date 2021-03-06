using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Infra.Data.Context;

namespace TimeControl.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20160422171049_TableProject")]
    partial class TableProject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("TimeControl.Models.Activity", b =>
                {
                    b.Property<Guid>("ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastTimeWorked");

                    b.Property<string>("Link")
                        .IsRequired();

                    b.Property<string>("Observation");

                    b.Property<Guid?>("ProjectProjectId");

                    b.Property<string>("Responsible");

                    b.Property<Guid>("ResponsibleId");

                    b.Property<bool>("Status");

                    b.HasKey("ActivityId");
                });

            modelBuilder.Entity("TimeControl.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("TimeControl.Models.BelongToProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("MemberId");

                    b.Property<string>("MemberId1");

                    b.Property<Guid>("ProjectId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("TimeControl.Models.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectName")
                        .IsRequired();

                    b.HasKey("ProjectId");
                });

            modelBuilder.Entity("TimeControl.Models.Time", b =>
                {
                    b.Property<Guid>("TimeId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActivityId");

                    b.Property<string>("ActivityTime");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<bool>("Status");

                    b.HasKey("TimeId");
                });

            modelBuilder.Entity("TimeControl.Models.UsersProjects", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AdministratorId");

                    b.Property<string>("AdministratorId1");

                    b.Property<Guid>("ProjectId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TimeControl.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TimeControl.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("TimeControl.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TimeControl.Models.Activity", b =>
                {
                    b.HasOne("TimeControl.Models.Project")
                        .WithMany()
                        .HasForeignKey("ProjectProjectId");
                });

            modelBuilder.Entity("TimeControl.Models.BelongToProject", b =>
                {
                    b.HasOne("TimeControl.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("MemberId1");

                    b.HasOne("TimeControl.Models.UsersProjects")
                        .WithOne()
                        .HasForeignKey("TimeControl.Models.BelongToProject", "ProjectId");
                });

            modelBuilder.Entity("TimeControl.Models.Time", b =>
                {
                    b.HasOne("TimeControl.Models.Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId");
                });

            modelBuilder.Entity("TimeControl.Models.UsersProjects", b =>
                {
                    b.HasOne("TimeControl.Models.ApplicationUser")
                        .WithOne()
                        .HasForeignKey("TimeControl.Models.UsersProjects", "AdministratorId1");

                    b.HasOne("TimeControl.Models.Project")
                        .WithOne()
                        .HasForeignKey("TimeControl.Models.UsersProjects", "ProjectId");
                });
        }
    }
}
