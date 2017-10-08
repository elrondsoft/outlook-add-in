using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Helios.Api.EFContext;

namespace Helios.Api.Migrations
{
    [DbContext(typeof(HeliosDbContext))]
    partial class HeliosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Helios.Api.Domain.Entities.MainModule.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiKey");

                    b.Property<string>("EntityId");

                    b.Property<string>("EventsSyncHash");

                    b.Property<string>("HeliosLogin");

                    b.Property<string>("HeliosPassword");

                    b.Property<string>("HeliosRefreshToken");

                    b.Property<string>("HeliosToken");

                    b.Property<bool>("IsSyncEnabled");

                    b.Property<string>("MicrosoftRefreshToken");

                    b.Property<string>("MicrosoftToken");

                    b.Property<string>("TasksSyncHash");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
        }
    }
}
