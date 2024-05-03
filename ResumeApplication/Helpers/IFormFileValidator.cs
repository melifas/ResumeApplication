namespace ResumeApplication.Helpers
{
    /// <summary>
    /// Helper class to validate file types according to the their starting bytes. Works with <see cref="IFormFile"/>.
    /// Inherits from <see cref="FileValidator"/>
    /// </summary>
    public class IFormFileValidator : FileValidator
    {
        /// <summary>
        /// Checks if <paramref name="file"/> is a valid PDF file.
        /// </summary>
        /// <param name="file"> The <see cref="IFormFile"/> file. </param>
        /// <returns>
        /// True if file is a PDF otherwise false
        /// </returns>
        public static bool IsValidPdf(IFormFile file)
        {
            return file?.ContentType == MimeTypes.ApplicationPdf && IsValidFile(file, PdfByteArray);
        }

        /// <summary>
        /// Checks if <paramref name="file"/> is a valid DOCX/PPTX/XLSX file.
        /// </summary>
        /// <param name="file"> The <see cref="IFormFile"/> file. </param>
        /// <returns>
        /// True if file is a DOCX/PPTX/XLSX otherwise false
        /// </returns>
        public static bool IsValidDocx(IFormFile file)
        {
            return file?.ContentType == MimeTypes.ApplicationWordOpenXml && IsValidFile(file, DocxByteArray);
        }

        /// <summary>
        /// Checks the starting bytes of <paramref name="file"/> to the starting bytes of a specific file type
        /// </summary>
        /// <param name="file"> The file. </param>
        /// <param name="fileTypeByteArray"> The file type starting bytes. </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsValidFile(IFormFile file, byte[] fileTypeByteArray)
        {
            using var ms = new MemoryStream();

            file.CopyTo(ms);
            var fileBytes = ms.ToArray();

            return IsValidByteLength(fileBytes, fileTypeByteArray);
        }
    }
}
