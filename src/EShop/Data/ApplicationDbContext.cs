using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EShop.Models;

namespace EShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public Guid Guid = System.Guid.NewGuid();
        public virtual DbSet<Category> Categories { set; get; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasOne(x => x.ParentCategory)
                .WithMany(x => x.ChildCategories)
                .HasForeignKey(x => x.ParentCategoryId);

            builder.Entity<Item>()
                // issireiskimo vertimas. Item turi viena Categorija, kuriai priklauso. Si kategorija, turi daug items, o items ForeignKey yra CategoryId
                .HasOne(x => x.Category)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.CategoryId);

            builder.Entity<Item>()
                .Property(x => x.Price)
                .HasColumnType("decimal(10,2)");

            builder.Entity<ShipmentItem>()
                .HasOne(x => x.Shipment)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.ShipmentId);

            builder.Entity<ShipmentItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.ShipmentItems)
                .HasForeignKey(x => x.ItemId);

            builder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);

            builder.Entity<OrderItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.ItemId);
        }
    }
}
