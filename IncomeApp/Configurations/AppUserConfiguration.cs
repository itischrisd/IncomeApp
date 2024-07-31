using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeApp.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(u => u.IdUser);

        builder.Property(u => u.IdUser)
            .UseIdentityColumn();

        builder.Property(u => u.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Password)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Salt)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.RefreshToken)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Roles)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
            );

        var users = new List<AppUser>
        {
            new()
            {
                IdUser = 3,
                Email = "admin1@mail.com",
                Password = "mI5uN+iedEO812e5shKzHyN5plRJgwHvc2FVMaVJmoc=",
                Salt = "1UBYrhohNWE3EvyvlBYtNg==",
                RefreshToken = "",
                Roles = "admin".Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
            },
            new()
            {
                IdUser = 4,
                Email = "user1@mail.com",
                Password = "SHboj+Zw3hXxfYq0xftwIeiUHp8bNgLUqxLGU6gvseo=",
                Salt = "E9pkXFJrthlcl3WamfJsXw==",
                RefreshToken = "",
                Roles = "user".Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
            }
        };

        builder.HasData(users);
    }
}