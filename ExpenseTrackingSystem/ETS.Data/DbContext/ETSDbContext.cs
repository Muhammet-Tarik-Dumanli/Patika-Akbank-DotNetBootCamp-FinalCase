using Microsoft.EntityFrameworkCore;
using ETS.Data;

namespace ETS.Data;
public class ETSDbContext : DbContext
{
    public ETSDbContext(DbContextOptions<ETSDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseCategoryConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}

