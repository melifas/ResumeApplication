using ResumeApplication.Entities;
using ResumeApplication.Models.ServiceModels.Degree;

using System.Collections.ObjectModel;

namespace ResumeApplication.Interfaces
{
    /// <summary>
    /// The degree account service interface.
    /// </summary>
    public interface IDegreeService
    {

        /// <summary>
        /// Get the degrees.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/> of <see cref="Degree"/>.
        /// </returns>
        Task<ReadOnlyCollection<Degree>> GetDegreesAsync();

        /// <summary>
        /// Get the degree.
        /// </summary>
        /// <param name="degreeId"> The degree id. </param>
        /// <returns>
        /// The <see cref="Task"/> of <see cref="GetDegreeResponse"/>.
        /// </returns>
        Task<GetDegreeResponse> GetDegreeAsync(int degreeId);

		/// <summary>
		/// Adds a degree.
		/// </summary>
		/// <param name="degreeModel"> The <see cref="Degree"/> </param>
		/// <returns> The identifier of the created brand </returns>
		Task<int> AddDegreeAsync(Degree degreeModel);

		/// <summary>
		/// Updates a degree.
		/// </summary>
		/// <param name="degreeModel"> The <see cref="Degree"/> </param>
		/// <returns>
		/// The <see cref="Task"/>
		/// </returns>
		Task UpdateDegreeAsync(Degree degreeModel);

		/// <summary>
		/// Deletes a degree.
		/// </summary>
		/// <param name="degreeId"> The degree id. </param>
		/// <returns>
		/// The <see cref="Task"/>
		/// </returns>
		Task DeleteDegreeAsync(int degreeId);


	}
}
