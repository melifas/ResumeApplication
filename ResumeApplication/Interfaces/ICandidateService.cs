using ResumeApplication.Models.ServiceModels.Degree;
using System.Collections.ObjectModel;
using ResumeApplication.Entities;
using ResumeApplication.Models;
using ResumeApplication.Models.ViewModels.Candidate;

namespace ResumeApplication.Interfaces
{
    /// <summary>
    /// The candidate service interface.
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
		/// Get the candidate.
		/// </summary>
		/// <param name="candidateId"> The candidate id. </param>
		/// <returns>
		/// The <see cref="Task"/> of <see cref="GetDegreeResponse"/>.
		/// </returns>
		Task<Candidate> GetCandidateAsync(int candidateId);

		/// <summary>
		/// Adds a candidate.
		/// </summary>
		/// <param name="candidateModel"> The <see cref="AddCandidateModel"/> </param>
		/// <returns> The identifier of the created brand </returns>
		Task<int> AddCandidateAsync(AddCandidateViewModel candidateModel);


		/// <summary>
		/// Updates a candidate.
		/// </summary>
		/// <param name="editCandidateModel"> The <see cref="EditCandidateModel"/> </param>
		/// <returns>
		/// The <see cref="Task"/>
		/// </returns>
		Task UpdateCandidateAsync(EditCandidateModel editCandidateModel);

		/// <summary>
		/// Deletes a candidate.
		/// </summary>
		/// <param name="candidateId"> The candidate id. </param>
		/// <returns>
		/// The <see cref="Task"/>
		/// </returns>
		Task DeleteCandidateAsync(int candidateId);

	}
}
