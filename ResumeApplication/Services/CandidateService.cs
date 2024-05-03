using ResumeApplication.Context;
using ResumeApplication.Entities;
using ResumeApplication.Interfaces;
using ResumeApplication.Models.ServiceModels.Degree;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace ResumeApplication.Services
{
    /// <summary>
    /// Implements the <see cref="ICandidateService"/> <see langword="interface"/>
    /// </summary>
    public class CandidateService : ICandidateService
    {
        /// <summary>
        /// Gets the <see cref="AppDbContext"/>.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateService"/> class.
        /// </summary>
        /// <param name="context"> The <see cref="AppDbContext"/> </param>
        public CandidateService(AppDbContext context)
        {
            _context = context;
        }

       
    }
}
