namespace ResumeApplication.Helpers
{
	/// <summary>
	/// Helper class for converting files.
	/// </summary>
	public static class FileConverter
	{
		/// <summary>
		/// Converts the <paramref name="files"/> to base64 encoded strings.
		/// </summary>
		/// <param name="files"> The list of <see cref="IFormFile"/> files. </param>
		/// <returns>
		/// The files as base64 encoded strings.
		/// </returns>
		public static List<string> ConvertToBase64(IEnumerable<IFormFile> files)
		{
			return files.Select(ConvertToBase64).ToList();
		}

		/// <summary>
		/// Converts the <paramref name="file"/> to a base64 encoded string.
		/// </summary>
		/// <param name="file"> The <see cref="IFormFile"/> file. </param>
		/// <returns>
		/// The file as base64 encoded string.
		/// </returns>
		public static string ConvertToBase64(IFormFile file)
		{
			if (file?.Length > 0)
			{
				using (var ms = new MemoryStream())
				{
					file.CopyTo(ms);

					var fileBytes = ms.ToArray();
					var s = Convert.ToBase64String(fileBytes);

					return s;
				}
			}

			return null;
		}
	}
}
