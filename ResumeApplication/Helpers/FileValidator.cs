namespace ResumeApplication.Helpers
{
    public class FileValidator
    {
        /// <summary>
        /// Gets the starting bytes of a PDF file.
        /// </summary>
        protected static byte[] PdfByteArray { get; } = { 37, 80, 68, 70 };

        /// <summary>
        /// Gets the starting bytes of a DOCX/PPTX/XLSX file.
        /// </summary>
        protected static byte[] DocxByteArray { get; } = { 80, 75, 3, 4, 20 };

        /// <summary>
        /// Checks if specified file is a valid PDF file.
        /// </summary>
        /// <param name="base64EncodedFile"> The base 64 encoded file. </param>
        /// <returns>
        /// True if file is a PDF otherwise false.
        /// </returns>
        public static bool IsValidPdf(string base64EncodedFile)
        {
            return !string.IsNullOrWhiteSpace(base64EncodedFile) && IsValidPdf(Convert.FromBase64String(base64EncodedFile));
        }

        /// <summary>
        /// Checks if specified file is a valid PDF file.
        /// </summary>
        /// <param name="fileByteArray"> The file in bytes. </param>
        /// <returns>
        /// True if file is a PDF otherwise false.
        /// </returns>
        public static bool IsValidPdf(byte[] fileByteArray)
        {
            return IsValidByteLength(fileByteArray, PdfByteArray);
        }

        /// <summary>
        /// Checks if specified file is a valid DOCX/PPTX/XLSX file.
        /// </summary>
        /// <param name="base64EncodedFile"> The base 64 encoded file. </param>
        /// <returns>
        /// True if file is a DOCX/PPTX/XLSX otherwise false.
        /// </returns>
        public static bool IsValidDocx(string base64EncodedFile)
        {
            return !string.IsNullOrWhiteSpace(base64EncodedFile) && IsValidDocx(Convert.FromBase64String(base64EncodedFile));
        }

        /// <summary>
        /// Checks if specified file is a valid DOCX/PPTX/XLSX file.
        /// </summary>
        /// <param name="fileByteArray"> The file in bytes. </param>
        /// <returns>
        /// True if file is a DOCX/PPTX/XLSX otherwise false.
        /// </returns>
        public static bool IsValidDocx(byte[] fileByteArray)
        {
            return IsValidByteLength(fileByteArray, DocxByteArray);
        }

        /// <summary>
        /// Compares the starting bytes of a file to the starting bytes of a specific file type
        /// </summary>
        /// <param name="fileByteArray"> The file in bytes. </param>
        /// <param name="fileTypeByteArray"> The file type starting bytes. </param>
        /// <returns>
        /// True if bytes match otherwise false.
        /// </returns>
        protected static bool IsValidByteLength(byte[] fileByteArray, byte[] fileTypeByteArray)
        {
            if (fileByteArray.Length < fileTypeByteArray.Length)
            {
                return false;
            }

            for (var i = 0; i < fileTypeByteArray.Length; i++)
            {
                if (fileByteArray[i] != fileTypeByteArray[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
