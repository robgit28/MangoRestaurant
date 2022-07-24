﻿// <auto-generated />
using Mango.Services.ProductAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220721133459_SeedProducts")]
    partial class SeedProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mango.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Stsrter",
                            Description = "fried pastry with a savory filling of spiced potatoes, onions, and peas",
                            ImageUrl = "https://robgit28.blob.core.windows.net/mango/samosas.jpeg",
                            Name = "Samosas",
                            Price = 3.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Stsrter",
                            Description = "Crispy Onion Bhajis with thinly sliced onions, mixed and coated in an chickpea flour batter",
                            ImageUrl = "https://robgit28.blob.core.windows.net/mango/onionBhajis.jpeg",
                            Name = "Onion Bhajis",
                            Price = 3.5
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "Stsrter",
                            Description = "Paneer pakoras fried, cheesy squares, served with green & red chili chutney",
                            ImageUrl = "https://robgit28.blob.core.windows.net/mango/paneerPakora.jpeg",
                            Name = "Paneer Pakora",
                            Price = 2.5
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "Stsrter",
                            Description = "Potato Cauliflower Vegetable Curry",
                            ImageUrl = "https://robgit28.blob.core.windows.net/mango/alooGobi.jpeg",
                            Name = "Aloo Gobi",
                            Price = 4.5
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
