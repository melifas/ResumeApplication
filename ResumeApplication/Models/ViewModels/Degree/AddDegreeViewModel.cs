using System.ComponentModel.DataAnnotations;

namespace ResumeApplication.Models.ViewModels.Degree
{
    public class AddDegreeViewModel
    {
        /// <summary>
        /// The degree title
        /// </summary>
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string Name { get; set; }

        public string? CandidateId { get; set; }

        /// <summary>
        /// The creation time
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? CreationTime { get; set; }

    }
}
