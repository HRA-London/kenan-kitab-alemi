using BookShopping.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookShopping.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feature> Features { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Priority).IsRequired();
                entity.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(c => c.Created).IsRequired().HasDefaultValueSql("GETDATE()");

            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.ShortDesc).IsRequired();
                entity.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(c => c.Created).IsRequired().HasDefaultValueSql("GETDATE()");

            });


            
        }
    }
}

