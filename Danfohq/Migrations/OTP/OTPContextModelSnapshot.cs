﻿// <auto-generated />
using Danfohq.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Danfohq.Migrations.OTP
{
    [DbContext(typeof(OTPContext))]
    partial class OTPContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Danfohq.Models.OTP", b =>
                {
                    b.Property<string>("phoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("verificationStatus")
                        .HasColumnType("boolean");

                    b.HasKey("phoneNumber");

                    b.ToTable("Otps");
                });
#pragma warning restore 612, 618
        }
    }
}
