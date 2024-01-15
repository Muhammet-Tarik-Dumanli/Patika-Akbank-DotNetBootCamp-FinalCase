using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace ETS.Data;

public class ExpenseCategory
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public List<Expense> Expenses { get; set; } = new List<Expense>();
}

public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.ToTable("ExpenseCategory", "dbo");

        builder.Property(x => x.CategoryId).IsRequired(true);
        builder.Property(x => x.CategoryName).IsRequired(true).HasMaxLength(50);

        builder.HasMany(x => x.Expenses)
            .WithOne(e => e.Category)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired(true);
    }
}
