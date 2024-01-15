using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ETS.Data;

public class Payment
{
    public int PaymentId { get; set; }
    public int ExpenseId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public string TransactionId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Expense Expense { get; set; }
}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment", "dbo");

        builder.Property(x => x.PaymentId).IsRequired(true);
        builder.Property(x => x.ExpenseId).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18, 4);
        builder.Property(x => x.PaymentDate).IsRequired(true);
        builder.Property(x => x.PaymentMethod).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.TransactionId).IsRequired(false).HasMaxLength(255);
        builder.Property(x => x.CreatedAt).IsRequired(true);

        builder.HasOne(x => x.Expense)
            .WithMany(e => e.Payments)
            .HasForeignKey(x => x.ExpenseId)
            .IsRequired(true);
    }
}
