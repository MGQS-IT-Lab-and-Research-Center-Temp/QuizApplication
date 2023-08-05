using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Entities;

namespace QuizApplication.Context.EntityConfiguration
{
    public class SubjectEntityTypeEntityConfig : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subjects");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(s => s.Name)
             .IsUnique();

            builder.Property(s => s.Description)
                .HasMaxLength(200);

            builder.HasMany(s => s.Questions)
                   .WithOne(q => q.Subject)
                   .HasForeignKey(u => u.SubjectId);
        }
    }

}
