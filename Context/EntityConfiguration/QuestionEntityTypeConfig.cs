using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Entities;

namespace QuizApplication.Context.EntityConfiguration
{
    public class QuestionEntityTypeConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(q => q.Id);

            builder.Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(q => q.OptionA)
               .IsRequired();

            builder.Property(q => q.OptionB)
               .IsRequired();

            builder.Property(q => q.OptionC)
               .IsRequired();

            builder.Property(q => q.OptionD)
               .IsRequired();

            builder.Property(q => q.CorrectOption)
               .IsRequired();

            builder.HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.UserId)
                .IsRequired();
        }
    }
}
