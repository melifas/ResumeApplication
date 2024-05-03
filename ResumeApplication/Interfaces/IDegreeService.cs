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
        /// Gets the degrees.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/> of <see cref="Degree"/>.
        /// </returns>
        Task<ReadOnlyCollection<Degree>> GetDegreesAsync();

        /// <summary>
        /// Adds a degree.
        /// </summary>
        /// <param name="degreeModel"> The <see cref="Degree"/> </param>
        /// <returns> The identifier of the created brand </returns>
        Task<int> AddDegreeAsync(Degree degreeModel);
    }
}
