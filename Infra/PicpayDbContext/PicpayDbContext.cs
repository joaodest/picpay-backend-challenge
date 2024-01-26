using Microsoft.EntityFrameworkCore;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.ValueObjects;
using PicpayChallenge.Helpers;
using System.Xml.Linq;

namespace PicpayChallenge.Infra
{
    public class PicpayDbContext : DbContext
    {
        public PicpayDbContext(DbContextOptions<PicpayDbContext> options, IConfiguration configuration)
            : base(options) { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Register default users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    Email = "johndoe@example.com",
                    CPF_CNPJ = "12345678953",
                    Password = "password123",
                    Balance = 100.00,
                    UserType = UserType.Normal
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Jane Smith",
                    Email = "janesmith@example.com",
                    CPF_CNPJ = "12332678921",
                    Password = "password123",
                    Balance = 100.00,
                    UserType = UserType.Normal
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Merchant",
                    Email = "merchant@example.com",
                    CPF_CNPJ = "98765432132145",
                    Password = "merchant123",
                    Balance = 76400,
                    UserType = UserType.Logista
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Normal User",
                    Email = "normaluser@example.com",
                    CPF_CNPJ = "56789012334",
                    Password = "normaluser123",
                    Balance = 0.00,
                    UserType = UserType.Normal
                }
            ); ;

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.FromUser)
                .WithMany(u => u.FromTransactions)
                .HasForeignKey(t => t.FromUserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ToUser)
                .WithMany(u => u.ToTransactions)
                .HasForeignKey(t => t.ToUserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

