using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TechEcommerce
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Rules> Rules { get; set; }
        public virtual DbSet<TypeProducts> TypeProducts { get; set; }
        public virtual DbSet<Utents> Utents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .Property(e => e.CodeProducts)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.IdProducts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rules>()
                .HasMany(e => e.Utents)
                .WithRequired(e => e.Rules)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeProducts>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.TypeProducts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utents>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Utents)
                .WillCascadeOnDelete(false);
        }
    }
}
