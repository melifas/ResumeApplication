namespace ResumeApplication
{
    /// <summary>
    /// App constants.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// The ResumeDb app setting key.
        /// </summary>
        public static readonly string ResumeDb = "ResumeDb";

		/// <summary>
		/// The phone regex. SHOULD FOLLOW that pattern. For the shake of project i added the 10 digit validation (E.164 standard) see https://stackoverflow.com/questions/6478875/regular-expression-matching-e-164-formatted-phone-numbers
		/// </summary>
		public const string PhoneRegEx = @"^\d{10}$";
	}
}
