
using SysVentas.Products.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SysVentas.Products.Infrastructure.Data.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Product> builder)
        {
            builder.ToTable(nameof(Products), ProductDataContext.DefaultSchema);
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Category)
                .WithMany(t => t.Products)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
