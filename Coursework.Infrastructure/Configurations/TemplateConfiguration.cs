using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursework.Infrastructure.Configurations;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        
        builder.HasMany(t => t.Tags)
            .WithOne(tt => tt.Template)
            .HasForeignKey(tt => tt.TemplateId);

        builder.HasMany(t => t.Likes)
            .WithOne(l => l.Template)
            .HasForeignKey(l => l.TemplateId);

        builder.HasMany(t => t.Comments)
            .WithOne(c => c.Template)
            .HasForeignKey(c => c.TemplateId);

        builder.HasMany(t => t.Forms)
            .WithOne(f => f.Template)
            .HasForeignKey(f => f.TemplateId);

        builder.HasOne(t => t.Author)
            .WithMany(a => a.Templates)
            .HasForeignKey(t => t.AuthorId);
    }
}