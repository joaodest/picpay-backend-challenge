using Microsoft.EntityFrameworkCore;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Helpers;
using System.Xml.Linq;

namespace PicpayChallenge.Infra
{
    public class PicpayDbContext : DbContext
    {
        public PicpayDbContext(DbContextOptions<PicpayDbContext> options, IConfiguration configuration)
            : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Transaction>()
            .HasOne(t => t.FromUser) 
            .WithMany(u => u.FromTransactions) 
            .HasForeignKey(t => t.FromUserId) 
            .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Transaction>()
            .HasOne(t => t.ToUser) 
            .WithMany(u => u.ToTransactions) 
            .HasForeignKey(t => t.ToUserId) 
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}

