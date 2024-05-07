using Microsoft.AspNetCore.Antiforgery;

namespace ResumeApplication.Helpers
{
	/// <summary>
	/// Provides extension methods for <see cref="HttpContext"/>.
	/// </summary>
	public static class HttpContextExtensions
	{
		/// <summary>
		/// Creates an antiforgery token.
		/// </summary>
		/// <param name="httpContext"> The <see cref="HttpContext"/>. </param>
		/// <returns>
		/// The <see cref="AntiforgeryTokenSet"/>.
		/// </returns>
		public static AntiforgeryTokenSet GetAntiforgeryToken(this HttpContext httpContext)
		{
			var antiforgeryService = (IAntiforgery)httpContext.RequestServices.GetService(typeof(IAntiforgery));

			// TODO/REVIEW: Investigate GetTokens or GetAndStoreTokens
			// good read -> https://www.dotnetcurry.com/aspnet/1343/aspnet-core-csrf-antiforgery-token
			// var tokenSet = antiforgeryService.GetAndStoreTokens(httpContext);
			var tokenSet = antiforgeryService.GetTokens(httpContext);

			return tokenSet;
		}
	}
}
