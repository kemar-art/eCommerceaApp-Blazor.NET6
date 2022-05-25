using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eCommerce.API.Date
{
    public partial class eCommerceDBContext : IdentityDbContext<ApiUser>
    {
        public eCommerceDBContext()
        {
        }

        public eCommerceDBContext(DbContextOptions<eCommerceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Cart_ToTable");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("cartItem");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK_cartItem_ToTable");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_cartItem_ToTable_1");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.DateJoined)
                    .HasColumnType("date")
                    .HasColumnName("dateJoined");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.DateOfOrder)
                    .HasColumnType("date")
                    .HasColumnName("dateOfOrder");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_ToTable");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_ToTable");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderDetail_ToTable_1");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .HasColumnName("paymentType");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Payment_ToTable");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(50)
                    .HasColumnName("imageURL");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Size).HasMaxLength(10);

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_ToTable_1");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Product_ToTable");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                { 
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "5ebe1e7b-8c70-44f4-ad8f-52a7f5b1c665"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "b2ae64a2-e75d-471b-9beb-3ad2051100a7"
                }
             );

            var hasher = new PasswordHasher<ApiUser>();
            modelBuilder.Entity<ApiUser>().HasData(
                new ApiUser
                {
                    Id = "2f6d6dd3-d5c5-47e6-958f-f0e132155645",
                    Email = "admin@ecommerce.com",
                    NormalizedEmail = "ADMIN@ECOMMERCE.COM",
                    UserName = "admin@ecommerce.com",
                    NormalizedUserName = "ADMIN@ECOMMERCE.COM",
                    firstName = "System",
                    lastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "Admin@1237")
                },
                new ApiUser
                {
                    Id = "ef393311-9f6c-46ac-b8ea-bc2a019b8cf6",
                    Email = "user@ecommerce.com",
                    NormalizedEmail = "USER@ECOMMERCE.COM",
                    UserName = "user@ecommerce.com",
                    NormalizedUserName = "USER@ECOMMERCE.COM",
                    firstName = "System",
                    lastName = "User",
                    PasswordHash = hasher.HashPassword(null, "User@1237")
                }
             );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "5ebe1e7b-8c70-44f4-ad8f-52a7f5b1c665",
                    UserId = "2f6d6dd3-d5c5-47e6-958f-f0e132155645"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "b2ae64a2-e75d-471b-9beb-3ad2051100a7",
                    UserId = "ef393311-9f6c-46ac-b8ea-bc2a019b8cf6"
                }
             );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
