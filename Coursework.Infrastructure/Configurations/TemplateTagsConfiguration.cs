using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursework.Infrastructure.Configurations;

public class TemplateTagsConfiguration : IEntityTypeConfiguration<TemplatesTags>
{
    public void Configure(EntityTypeBuilder<TemplatesTags> builder)
    {
        builder.HasKey(tt => tt.Id);
        builder.Property(tt => tt.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(tt => tt.Template)
            .WithMany(t => t.Tags)
            .HasForeignKey(tt => tt.TemplateId);
        
        builder.HasOne(tt => tt.Tag)
            .WithMany(t =>t.Templates)
            .HasForeignKey(tt => tt.TagId);
    }
}