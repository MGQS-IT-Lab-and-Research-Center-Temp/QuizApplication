using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Entities;

namespace QuizApplication.Context.EntityConfiguration
{
    public class SubjectQuestionEntityTypeConfig : IEntityTypeConfiguration<SubjectQuestion>
    {
        public void Configure(EntityTypeBuilder<SubjectQuestion> builder)
        {
            builder.ToTable("CategoryQuestions");
            builder.Ignore(cq => cq.Id);
            builder.HasKey(cq => new { cq.SubjectId, cq.QuestionId });

            builder.HasOne(cq => cq.Subject)
                .WithMany(c => c.SubjectQuestions)
                .HasForeignKey(cq => cq.SubjectId)
                .IsRequired();

            builder.HasOne(cq => cq.Question)
                .WithMany(q => q.SubjectQuestions)
                .HasForeignKey(cq => cq.QuestionId)
                .IsRequired();
        }
    }

}
