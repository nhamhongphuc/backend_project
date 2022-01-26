using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using MySql.Data.MySqlClient;

namespace BackEnd.Data
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account").HasKey(c => c.AccountID);
            modelBuilder.Entity<Address>().ToTable("Address").HasKey(c => c.AddressID);
            modelBuilder.Entity<Cart>().ToTable("Cart").HasKey(c => c.CartID);          
            modelBuilder.Entity<Order>().ToTable("Order_").HasKey(c => c.OrderID);
            modelBuilder.Entity<Product>().ToTable("Product").HasKey(c => c.ProductID);
            modelBuilder.Entity<ProductType>().ToTable("ProductType").HasKey(c => c.ProductTypeID);
            modelBuilder.Entity<SearchHistory>().ToTable("SearchHistory").HasKey(c => c.SearchHistoryID);
            modelBuilder.Entity<Supplier>().ToTable("Supplier").HasKey(c => c.SupplierID);
            modelBuilder.Entity<User>().ToTable("User").HasKey(c => c.UserID);


            modelBuilder.Entity<CartDetail>().ToTable("CartDetail").HasKey(c => new { c.CartID, c.ProductID});
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail").HasKey(c => new { c.OrderID, c.ProductID });
            modelBuilder.Entity<Review>().ToTable("Review").HasKey(c => new { c.ProductID, c.AccountID });
      
        }
       
    }
}
