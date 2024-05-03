using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.CompilerServices;
using ResumeApplication.ValidationAttributes;

namespace ResumeApplication.Models.ViewModels.Candidate
{
    public class AddCandidateViewModel
    {

        /// <summary>
        /// The first name of the candidate
        /// </summary>
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the candidate
        /// </summary>
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// The email of the candidate
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        /// <summary>
        /// The candidate files to add
        /// </summary>
        [AllowedMimeTypes(true, new[] { MimeTypes.PdfExtension, MimeTypes.DocxExtension })]
        [Display(Name = "Upload Files")]
        public List<IFormFile> FilesToAdd { get; init; } = new();
    }
}
