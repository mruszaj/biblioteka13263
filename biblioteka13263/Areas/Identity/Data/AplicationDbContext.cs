using biblioteka13263.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace biblioteka13263.Areas.Identity.Data;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //builder.Property(x => x.Email).IsRequired();
        //builder.Property(x => x.Password).IsRequired();
    }
}
public class AplicationDbContext : IdentityDbContext<User>
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.Entity<User>()
           .HasIndex(b => b.Email)
           .IsUnique();
    }

}
