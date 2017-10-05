using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Helios.Api.EFContext;

namespace Helios.Api.Migrations
{
    [DbContext(typeof(HeliosDbContext))]
    [Migration("20170914090535_Initial-Create")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Helios.Api.EFContext.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HeliosLogin");

                    b.Property<string>("HeliosPassword");

                    b.Property<string>("HeliosRefreshToken");

                    b.Property<string>("HeliosToken");

                    b.Property<bool>("IsSyncEnabled");

                    b.Property<string>("MicrosoftLogin");

                    b.Property<string>("MicrosoftRefreshToken");

                    b.Property<string>("MicrosoftToken");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
        }
    }
}
