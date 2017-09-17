using InventNET.Desktop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventNET.Desktop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["InventNETDatabase"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductID);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.MinimumQuantity).HasDefaultValue(0).IsRequired();
                entity.Property(p => p.AvailableQuantity).HasDefaultValue(0).IsRequired();
                entity.Property(p => p.BuyingPrice).HasDefaultValue(0.00).IsRequired();
                entity.Property(p => p.SellingPrice).HasDefaultValue(0.00).IsRequired();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(p => p.TransactionID);
                entity.Property(p => p.Quantity).IsRequired();
                entity.Property(p => p.Type).IsRequired();
                entity.Property(p => p.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<ProductTransaction>(entity =>
            {
                entity.HasKey(t => new { t.ProductID, t.TransactionID });

                entity.HasOne(p => p.Product)
                    .WithMany(p => p.ProductTransactions)
                    .HasForeignKey(p => p.ProductID);

                entity.HasOne(p => p.Transaction)
                    .WithMany(p => p.ProductTransactions)
                    .HasForeignKey(p => p.TransactionID);
            });
        }
    }
}
