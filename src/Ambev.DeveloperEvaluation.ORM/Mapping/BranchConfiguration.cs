using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(b => b.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(b => b.Address)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(b => b.IsActive)
                .IsRequired()
                .HasColumnType("boolean");

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(b => b.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.HasMany(b => b.Sales)         
                .WithOne(s => s.Branch)      
                .HasForeignKey(s => s.BranchId) 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}