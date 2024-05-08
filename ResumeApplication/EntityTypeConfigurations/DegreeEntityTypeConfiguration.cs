using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ResumeApplication.Entities;

namespace ResumeApplication.EntityTypeConfigurations
{
    /// <summary>
    /// The <see cref="IEntityTypeConfiguration{T}"/> where T is <see cref="Degree"/>.
    /// </summary>
    public class DegreeEntityTypeConfiguration : IEntityTypeConfiguration<Degree>
    {
        public void Configure(EntityTypeBuilder<Degree> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(255);

            builder.Property(e => e.CreationTime);

        }
    }
}
