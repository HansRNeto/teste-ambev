using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);

            builder.Property(si => si.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(si => si.SaleId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(si => si.ProductId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(si => si.ProductName)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(si => si.Quantity)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(si => si.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(si => si.DiscountPercentage)
                .HasColumnType("decimal(5,2)");

            builder.Property(si => si.DiscountAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(si => si.TotalAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(si => si.IsCancelled)
                .IsRequired()
                .HasColumnType("boolean");

            builder.Property(si => si.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(si => si.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasOne(si => si.Product)
                .WithMany(p => p.SaleItems)
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(si => si.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
