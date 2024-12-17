using Sunglass_ecom.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Sunglass_ecom.Data
{


    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext()
        {
        }

        // Constructor for dependency injection
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
            : base(options) { }

        // DbSet for each table
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: Configure table name
            modelBuilder.Entity<Registration>()
                .ToTable("Customer");
           
        
            modelBuilder.Entity<Cart>()
                .ToTable("Cart")
                .HasKey(c => c.Id); // Specify the primary key

            modelBuilder.Entity<OrderItems>()
                .ToTable("OrderItems")
               .HasKey(c => c.Id);

            modelBuilder.Entity<OrderItems>()
                .HasOne(o => o.Product)
                .WithOne(p => p.OrderItems);


            modelBuilder.Entity<Admins>()
                .ToTable("Admins")
               .HasKey(c => c.Id);


            modelBuilder.Entity<Product>()
                .ToTable("Product")
               .HasKey(c => c.Id);

               modelBuilder.Entity<Product>()
               .HasOne(p => p.Category)
            .WithMany(c => c.Product)
            .HasForeignKey(p => p.CategoryId)
            .HasForeignKey(p => p.CartId)
            .OnDelete(DeleteBehavior.Cascade); // Specify the primary key
            base.OnModelCreating(modelBuilder);

            // Define relationships
           




        }
    }
    
}

          
    



