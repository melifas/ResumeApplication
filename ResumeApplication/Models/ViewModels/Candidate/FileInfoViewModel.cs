namespace ResumeApplication.Models.ViewModels.Candidate
{
	/// <summary>
	/// The file info view model
	/// </summary>
	public class FileInfoViewModel
	{
		/// <summary>
		/// Gets or sets the base64 encoded file
		/// </summary>
		public string EncodedFile { get; init; }

		/// <summary>
		/// Gets or sets the file name
		/// </summary>
		public string FileName { get; init; }
	}
}
