using Coursework.Domain.Enums;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursework.Infrastructure.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).ValueGeneratedOnAdd();
        
        builder.Property(q => q.Type)
            .HasConversion<string>(
                s => s.ToString(),
                str => Enum.Parse<QuestionTypeEnum>(str));

        builder.HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId);
        
        builder.HasOne(q => q.Template)
            .WithMany(t => t.Questions)
            .HasForeignKey(q => q.TemplateId);
    }
}