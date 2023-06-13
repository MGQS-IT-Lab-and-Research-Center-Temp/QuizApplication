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

            builder.HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.UserId)
                .IsRequired();

            builder.Property(q => q.AskedQuestion)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(q => q.ImageUrl)
                .HasColumnType("varchar(255)");

            builder.HasMany(q => q.SubjectQuestions)
                .WithOne(cq => cq.Question)
                .IsRequired();

            builder.HasMany(q => q.Answers)
                .WithOne(c => c.Question)
                .IsRequired();

        }
    }
}
