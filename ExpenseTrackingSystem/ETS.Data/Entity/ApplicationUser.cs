using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETS.Data.Entity;

public class ApplicationUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int Status { get; set; }
}

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
    
        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Password).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Role).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.LastActivityDate).IsRequired(true);
        builder.Property(x => x.Status).IsRequired(true).HasDefaultValue(1);

        builder.HasIndex(x => x.UserName).IsUnique(true);

    }
}