using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumeApplication.Entities;

namespace ResumeApplication.EntityTypeConfigurations
{
    public class CandidateEntityTypeConfiguration : IEntityTypeConfiguration<Candidate>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.DegreeId);

            builder.Property(e => e.LastName).IsRequired().HasMaxLength(255);

            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(255);

            builder.Property(e => e.LastName).IsRequired().HasMaxLength(255);

            builder.Property(e => e.Email).HasMaxLength(100);

            builder.Property(e => e.Mobile).HasMaxLength(10);

            builder.Property(e => e.Cv).HasMaxLength(255);

            builder.Property(e => e.CreationTime);

            builder.HasOne(i => i.Degree).WithMany(a => a.Candidates).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
