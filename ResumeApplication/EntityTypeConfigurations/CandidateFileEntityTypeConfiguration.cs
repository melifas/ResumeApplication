using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ResumeApplication.Entities;

namespace ResumeApplication.EntityTypeConfigurations
{
    public class CandidateFileEntityTypeConfiguration : IEntityTypeConfiguration<CandidateFile>
    {
        public void Configure(EntityTypeBuilder<CandidateFile> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CandidateId);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);

            builder.Property(e => e.FileType).IsRequired().HasMaxLength(10).IsUnicode(false);

            builder.Property(e => e.DataFile).IsRequired();


            builder.HasOne(cr => cr.Candidate).WithOne(r => r.CandidateFile).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
