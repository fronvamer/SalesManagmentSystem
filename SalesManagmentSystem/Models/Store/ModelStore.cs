using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SalesManagmentSystem.Models.Store
{
    public partial class ModelStore : DbContext
    {
        public ModelStore()
            : base("name=ModelStore")
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Discounts> Discounts { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<SaleItems> SaleItems { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Warehouses> Warehouses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Customers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discounts>()
                .Property(e => e.ThresholdAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Discounts>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Employees)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.RetailPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Inventory)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.SaleItems)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Warehouses)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SaleItems>()
                .Property(e => e.SalePrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Sales>()
                .Property(e => e.TotalAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Sales>()
                .HasMany(e => e.SaleItems)
                .WithRequired(e => e.Sales)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stores>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Stores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stores>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Stores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stores>()
                .HasMany(e => e.Warehouses)
                .WithRequired(e => e.Stores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouses>()
                .HasMany(e => e.Inventory)
                .WithRequired(e => e.Warehouses)
                .WillCascadeOnDelete(false);
        }
    }
}
