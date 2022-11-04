using Data.EF.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.Contexts
{
    public class ShopContext : IdentityDbContext<User, Role, long>
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleProduct> SaleProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SaleProduct>(entity =>
            {
                entity.HasOne(x => x.Product).WithMany(x => x.SaleProduct)
                    .HasForeignKey("FK_SaleProduct_ProductId").IsRequired().OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Sale).WithMany(x => x.SaleProduct)
                    .HasForeignKey("FK_SaleProduct_SaleId").IsRequired().OnDelete(DeleteBehavior.NoAction);
                entity.Property(e => e.InsertDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModificationDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("(0)");
            });

            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.InsertDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModificationDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("(0)");
            });
            
            builder.Entity<Role>(entity =>
            {
                entity.Property(e => e.InsertDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModificationDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("(0)");
            });
            
            builder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.InsertDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModificationDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("(0)");
            });
            
            builder.Entity<Product>(entity =>
            {
                entity.Property(e => e.InsertDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ModificationDateUTC).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("(0)");
            });
            base.OnModelCreating(builder);
        }
    }
}