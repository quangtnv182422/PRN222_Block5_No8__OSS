using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OSS_Main.Models.Entity;

public partial class Prn222ProjectContext : IdentityDbContext<AspNetUser, AspNetRole, string>
{
    public Prn222ProjectContext()
    {
    }

    public Prn222ProjectContext(DbContextOptions<Prn222ProjectContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductSpec> ProductSpecs { get; set; }

    public virtual DbSet<ReceiverInformation> ReceiverInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(ConnectionString);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Carts_CustomerId");

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts).HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasIndex(e => e.CartId, "IX_CartItems_CartId");

            entity.HasIndex(e => e.ProductSpecId, "IX_CartItems_ProductSpecID");

            entity.Property(e => e.PriceEachItem).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductSpecId).HasColumnName("ProductSpecID");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasForeignKey(d => d.CartId);

            entity.HasOne(d => d.ProductSpec).WithMany(p => p.CartItems).HasForeignKey(d => d.ProductSpecId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Feedbacks_CustomerId");

            entity.HasIndex(e => e.ProductId, "IX_Feedbacks_ProductId");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Product).WithMany(p => p.Feedbacks).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

            entity.HasIndex(e => e.ReceiverId, "IX_Orders_ReceiverId");

            entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Receiver).WithMany(p => p.Orders).HasForeignKey(d => d.ReceiverId);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasIndex(e => e.CartItemId, "IX_OrderItems_CartItemId");

            entity.HasIndex(e => e.FeedbackId, "IX_OrderItems_FeedbackId");

            entity.HasIndex(e => e.OrderId, "IX_OrderItems_OrderId");


            entity.HasOne(d => d.CartItem).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.CartItemId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Feedback).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.FeedbackId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItemOrders).HasForeignKey(d => d.OrderId);

        });
        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId);
            entity.Property(e => e.OrderStatusName).HasMaxLength(100).IsRequired();
        });


        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductName).HasMaxLength(255);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_ProductCategories_CategoryId");

            entity.HasIndex(e => e.ProductId, "IX_ProductCategories_ProductId");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategories).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCategories).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductImages_ProductId");

            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<ProductSpec>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductSpecs_ProductId");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SpecName).HasMaxLength(255);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSpecs).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<ReceiverInformation>(entity =>
        {
            entity.HasKey(e => e.ReceiverId);  

            entity.HasOne(r => r.Customer) 
                  .WithMany(c => c.ReceiverInformations)  
                  .HasForeignKey(r => r.CustomerId); 
        });
        modelBuilder.Entity<Media>()
    .HasOne(m => m.Feedback)
    .WithMany(f => f.Medias)
    .HasForeignKey(m => m.FeedbackId);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
