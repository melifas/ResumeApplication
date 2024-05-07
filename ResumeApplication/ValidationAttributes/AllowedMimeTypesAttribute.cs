using System.ComponentModel.DataAnnotations;
using ResumeApplication.Helpers;

namespace ResumeApplication.ValidationAttributes
{
    /// <summary>
    /// Allowed file MIME types <see cref="ValidationAttribute"/>.
    /// Works with a single or multiple <see cref="IFormFile"/> files
    /// </summary>
    public class AllowedMimeTypesAttribute : ValidationAttribute
    {
        /// <summary>
        /// Indicates whether to work with a single or multiple files.
        /// </summary>
        private readonly bool _singleFile;

        /// <summary>
        /// The allowed MIME types.
        /// </summary>
        private readonly string[] _mimeTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllowedMimeTypesAttribute"/> class.
        /// </summary>
        /// <param name="singleFile"> Indicates whether to work with a single or multiple files. </param>
        /// <param name="mimeTypes"> The allowed MIME types. </param>
        public AllowedMimeTypesAttribute(bool singleFile, string[] mimeTypes)
        {
            _singleFile = singleFile;
            _mimeTypes = mimeTypes;
        }

        /// <summary>
        /// Validates the file(s).
        /// </summary>
        /// <param name="value"> The file(s). </param>
        /// <param name="validationContext"> The <see cref="ValidationContext"/>. </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_singleFile)
            {
                if (value is IFormFile file)
                {
                    var result = ValidateFile(file);

                    if (result != ValidationResult.Success)
                    {
                        return result;
                    }
                }
            }
            else
            {
                if (value is IList<IFormFile> files)
                {
                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            var result = ValidateFile(file);

                            if (result != ValidationResult.Success)
                            {
                                return result;
                            }
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }

        private static bool HasValidContents(IFormFile file)
        {
            var isValidFile = true;

            switch (file.ContentType)
            {
                case MimeTypes.ApplicationPdf:
                    isValidFile = IFormFileValidator.IsValidPdf(file);
                    break;
                case MimeTypes.DocxExtension:
                    isValidFile = IFormFileValidator.IsValidDocx(file);
                    break;
            }

            return isValidFile;
        }

        private ValidationResult ValidateFile(IFormFile file)
        {
            var result = ValidateFileIsInAllowedMimeTypes(file);

            if (result != ValidationResult.Success)
            {
                return result;
            }

            if (!HasValidContents(file))
            {
                result = new ValidationResult($"File {file.FileName} contents do not match file content type {file.ContentType}");
            }

            return result;
        }

        private ValidationResult ValidateFileIsInAllowedMimeTypes(IFormFile file)
        {
            return !_mimeTypes.Any(x => x.Equals(file.ContentType, StringComparison.InvariantCultureIgnoreCase))
                ? new ValidationResult($"{file.FileName} is of MIME type {file.ContentType} which is not allowed! Only {_mimeTypes.Aggregate((partial, type) => $"{partial}, {type}")} permitted.")
                : ValidationResult.Success;
        }
    }
}
