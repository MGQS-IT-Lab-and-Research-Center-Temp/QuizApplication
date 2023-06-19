﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Entities;

namespace QuizApplication.Context.EntityConfiguration
{
    public class SubjectEntityTypeEntityConfig : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subjects");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(c => c.Name)
             .IsUnique();

            builder.Property(c => c.Description)
                .HasMaxLength(200);
        }
    }

}
