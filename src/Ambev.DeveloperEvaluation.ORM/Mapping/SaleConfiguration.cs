using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.SaleId);
            builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(s => s.TotalAmount).HasColumnType("decimal(18,2)");
            builder.HasMany(s => s.Items).WithOne().HasForeignKey(i => i.SaleId);
        }
    }

    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.HasKey(si => si.SaleItemId);
            builder.Property(si => si.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(si => si.Discount).HasColumnType("decimal(18,2)");
        }
    }
}
