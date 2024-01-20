using ETS.Data.Entity;
using Microsoft.EntityFrameworkCore;

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

    public static void SeedData(ETSDbContext context)
    {
        if (!context.Users.Any())
        {
            string password = "194eb0fe1e2eb6cffa4bdc24d11a20a4";
            // Users tablosuna örnek veri ekleme
            var seedUserOne = new User { FirstName = "John", LastName = "Doe", Email = "john@example.com", Role = "admin", Password = password, UserName = "admin", IBAN = "1111111111" };
            context.Users.Add(seedUserOne);

            var seedUserTwo = new User { FirstName = "Jane", LastName = "Doe", Email = "jane@example.com", Role = "personal", Password = password, UserName = "personal", IBAN = "22222222" };
            context.Users.Add(seedUserTwo);

            context.SaveChanges();
        }
    }
}