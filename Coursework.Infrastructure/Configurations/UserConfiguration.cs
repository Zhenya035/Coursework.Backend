using Coursework.Domain.Enums;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursework.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        
        builder.Property(u => u.Status)
            .HasConversion<string>(
                s => s.ToString(),
                str => Enum.Parse<StatusEnum>(str));
        
        builder.Property(u => u.Role)
            .HasConversion<string>(
                s => s.ToString(),
                str => Enum.Parse<RoleEnum>(str));

        builder.HasMany(u => u.Comments)
            .WithOne(c => c.Author)
            .HasForeignKey(c => c.AuthorId);

        builder.HasMany(u => u.Templates)
            .WithOne(t => t.Author)
            .HasForeignKey(t => t.AuthorId);

        builder.HasMany(u => u.Forms)
            .WithOne(f => f.Author)
            .HasForeignKey(f => f.AuthorId);

        builder.HasMany(u => u.Likes)
            .WithOne(l => l.Author)
            .HasForeignKey(l => l.AuthorId);
    }
}