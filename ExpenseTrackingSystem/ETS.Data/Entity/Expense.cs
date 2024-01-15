using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ETS.Data;

public class Expense
{
    public int ExpenseId { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string PaymentMethod { get; set; }
    public string ReceiptUrl { get; set; }
    public string Status { get; set; } // Onay Bekliyor/Reddedildi/Onaylandı
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; }
    public ExpenseCategory Category { get; set; }
    public List<Payment> Payments { get; set; } = new List<Payment>();
}

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expense", "dbo");

        builder.Property(x => x.ExpenseId).IsRequired(true);
        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.CategoryId).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18, 4);
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(255);
        builder.Property(x => x.Location).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.PaymentMethod).IsRequired(false).HasMaxLength(50);
        builder.Property(x => x.ReceiptUrl).IsRequired(false).HasMaxLength(255);
        builder.Property(x => x.Status).IsRequired(true).HasMaxLength(20);
        builder.Property(x => x.CreatedAt).IsRequired(true);
        builder.Property(x => x.UpdatedAt).IsRequired(true);

        builder.HasOne(x => x.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(x => x.UserId)
            .IsRequired(true);

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .IsRequired(true);
    }
}
