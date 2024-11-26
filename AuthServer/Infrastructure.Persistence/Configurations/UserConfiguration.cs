using AuthServer.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(x => new { x.FirstName, x.LastName, x.MiddleName })
            .IsUnique();

        builder
            .Ignore(c => c.FirstName)
            .Ignore(c => c.LastName)
            .Ignore(c => c.MiddleName)
            .Ignore(c => c.NormalizedEmail)
            .Ignore(c => c.Email)
            .Ignore(c => c.EmailConfirmed)
            .Ignore(c => c.SecurityStamp)
            .Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.PhoneNumber)
            .Ignore(c => c.PhoneNumberConfirmed)
            .Ignore(c => c.TwoFactorEnabled)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.LockoutEnabled)
            .Ignore(c => c.AccessFailedCount);
    }
}