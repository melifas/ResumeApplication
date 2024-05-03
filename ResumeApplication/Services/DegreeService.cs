using Microsoft.EntityFrameworkCore;
using ResumeApplication.Context;
using ResumeApplication.Entities;
using ResumeApplication.Interfaces;
using ResumeApplication.Models.ServiceModels.Degree;
using System.Collections.ObjectModel;

namespace ResumeApplication.Services
{
    /// <summary>
    /// Implements the <see cref="IDegreeService"/> <see langword="interface"/>
    /// </summary>
    public class DegreeService : IDegreeService
    {
        /// <summary>
        /// Gets the <see cref="AppDbContext"/>.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateService"/> class.
        /// </summary>
        /// <param name="context"> The <see cref="AppDbContext"/> </param>
        public DegreeService(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<ReadOnlyCollection<Degree>> GetDegreesAsync()
        {
            var degrees = await _context.Degrees.AsNoTracking().ToListAsync().ConfigureAwait(false);

            return degrees.AsReadOnly();
        }

        /// <inheritdoc/>
        public async Task<int> AddDegreeAsync(Degree degreeModel)
        {
            if (degreeModel == null)
            {
                throw new ArgumentNullException(nameof(degreeModel), "Supplied model was null");
            }

            var degree = new Degree { Name = degreeModel.Name, CreationTime = DateTime.Now, CandidateId = degreeModel.CandidateId };

            try
            {
                await _context.Degrees.AddAsync(degree);

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //logging here..

                throw;
            }

            return degree.Id;
        }

    }
}
