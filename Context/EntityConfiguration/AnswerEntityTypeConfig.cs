using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Entities;

namespace QuizApplication.Context.EntityConfiguration
{
    public class AnswerEntityTypeConfig : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.AnswerText)
                   .IsRequired()
                   .HasColumnType("text");

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Answers)
                   .HasForeignKey(c => c.UserId)
                   .IsRequired();

            builder.HasOne(c => c.Question)
                   .WithMany(q => q.Answers)
                   .HasForeignKey(c => c.QuestionId)
                   .IsRequired();
        }
    }
}
