using ResumeApplication.Models.ServiceModels.Degree;
using System.Collections.ObjectModel;
using ResumeApplication.Entities;

namespace ResumeApplication.Interfaces
{
    /// <summary>
    /// The candidate account service interface.
    /// </summary>
    public interface ICandidateService
    {
		/// <summary>
		/// Gets the candidates.
		/// </summary>
		/// <returns>
		/// The <see cref="Task"/> of <see cref="Candidate"/>.
		/// </returns>
		Task<ReadOnlyCollection<Candidate>> GetCandidatesAsync();

		/// <summary>
		/// Adds a candidate.
		/// </summary>
		/// <param name="candidateModel"> The <see cref="Candidate"/> </param>
		/// <returns> The identifier of the created brand </returns>
		Task<int> AddCandidateAsync(Candidate candidateModel);

	}
}
