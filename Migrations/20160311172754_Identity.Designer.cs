using System;
using Infra.Data.Context;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Migrations;

namespace TimeControl.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20160311172754_Identity")]
    partial class Identity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("TimeControl.Models.Activity", b =>
                {
                    b.Property<Guid>("activityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Link");

                    b.Property<string>("Name");

                    b.Property<string>("Observation");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Time");

                    b.HasKey("activityId");
                });

            modelBuilder.Entity("TimeControl.Models.Responsible", b =>
                {
                    b.Property<Guid>("responsibleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("responsibleId");
                });
        }
    }
}
