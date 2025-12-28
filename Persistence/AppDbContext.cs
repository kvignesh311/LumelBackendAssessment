using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Customer>(e =>
            {
                e.Property(x => x.Name).HasMaxLength(100).IsRequired();
                e.Property(x => x.Email).HasMaxLength(100).IsRequired();
                e.Property(x => x.Address).HasMaxLength(200).IsRequired();
            });

            model.Entity<Product>(e =>
            {
                e.Property(x => x.Name).HasMaxLength(100).IsRequired();
                e.Property(x => x.Category).HasMaxLength(100).IsRequired();
            });

            model.Entity<Order>(e =>
            {
                e.Property(x => x.Region).HasMaxLength(100).IsRequired();
                e.Property(x => x.PaymentMethod).HasMaxLength(100).IsRequired();
                e.Property(x => x.SaleDate).IsRequired();
            });
        }
    }
}
