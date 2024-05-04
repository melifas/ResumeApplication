﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public string Email { get; set; }


        public string Moblie { get; set; }

        /// <summary>
        /// Gets or sets the degrees
        /// </summary>
        public List<SelectListItem> Degrees { get; set; } = [];

        /// <summary>
        /// The candidate file to add
        /// </summary>
        [AllowedMimeTypes(true, new[] { MimeTypes.PdfExtension, MimeTypes.DocxExtension })]
        [Display(Name = "Upload Cv")]
        public IFormFile AddCv { get; init; }
    }
}