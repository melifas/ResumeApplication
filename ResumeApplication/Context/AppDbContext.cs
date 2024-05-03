using Microsoft.EntityFrameworkCore;
using ResumeApplication.Entities;
using ResumeApplication.EntityTypeConfigurations;

namespace ResumeApplication.Context
{
    /// <summary>   
    /// The DbContext  <see cref="DbContext"/>.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        public AppDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options"> The <see cref="DbContextOptions{T}"/> where T is of type <see cref="AppDbContext"/>.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the candidates.
        /// </summary>
        public virtual DbSet<Candidate> Candidates { get; set; }

        /// <summary>
        /// Gets or sets the candidates files.
        /// </summary>
        public virtual DbSet<CandidateFile> CandidateFiles { get; set; }

        /// <summary>
        /// Gets or sets the degrees.
        /// </summary>
        public virtual DbSet<Degree> Degrees { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            new CandidateEntityTypeConfiguration().Configure(builder.Entity<Candidate>());
            new CandidateFileEntityTypeConfiguration().Configure(builder.Entity<CandidateFile>());
            new DegreeEntityTypeConfiguration().Configure(builder.Entity<Degree>());
        }

    }
}
