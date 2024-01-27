﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PicpayChallenge.Infra;

#nullable disable

namespace PicpayChallenge.Migrations
{
    [DbContext(typeof(PicpayDbContext))]
    [Migration("20240127142505_MergingTxsHist")]
    partial class MergingTxsHist
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PicpayChallenge.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double>("Balance")
                        .HasColumnType("double");

                    b.Property<string>("CPF_CNPJ")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5aff7013-4a16-47bc-bdca-8ef0bd692846"),
                            Balance = 100.0,
                            CPF_CNPJ = "12345678953",
                            Email = "johndoe@example.com",
                            Name = "John Doe",
                            Password = "password123",
                            UserType = 0
                        },
                        new
                        {
                            Id = new Guid("044a5fb0-608d-4009-9c2e-29d59422c5b6"),
                            Balance = 100.0,
                            CPF_CNPJ = "12332678921",
                            Email = "janesmith@example.com",
                            Name = "Jane Smith",
                            Password = "password123",
                            UserType = 0
                        },
                        new
                        {
                            Id = new Guid("18ef7ea5-2b37-44c0-8a08-ce18548f85e6"),
                            Balance = 76400.0,
                            CPF_CNPJ = "98765432132145",
                            Email = "merchant@example.com",
                            Name = "Merchant",
                            Password = "merchant123",
                            UserType = 1
                        },
                        new
                        {
                            Id = new Guid("fe0008f7-d268-48bf-af7a-d75193229a45"),
                            Balance = 0.0,
                            CPF_CNPJ = "56789012334",
                            Email = "normaluser@example.com",
                            Name = "Normal User",
                            Password = "normaluser123",
                            UserType = 0
                        });
                });

            modelBuilder.Entity("PicpayChallenge.Domain.ValueObjects.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FromUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ToUserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("PicpayChallenge.Domain.ValueObjects.Transaction", b =>
                {
                    b.HasOne("PicpayChallenge.Domain.Entities.User", "FromUser")
                        .WithMany("Transactions")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PicpayChallenge.Domain.Entities.User", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("PicpayChallenge.Domain.Entities.User", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}