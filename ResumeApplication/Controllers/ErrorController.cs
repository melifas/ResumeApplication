using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ResumeApplication.Models.ViewModels;
using System.Diagnostics;

namespace ResumeApplication.Controllers
{
	/// <summary>
	/// The error controller.
	/// </summary>
	// [AllowAnonymous]
	[Route("error")]
	public class ErrorController : Controller
	{
		

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorController"/> class.
		/// </summary>        
		public ErrorController()
		{
			
		}

		[Route("{statusCode}")]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult HttpStatusCodeHandler(int statusCode)
		{
			switch (statusCode)
			{
				case 404:
					// return View("Errors/NotFound", model);
					return View("Errors/NotFound");
				case 500:

					return View("Errors/InternalServerError");
				default:
					// return View("Errors/InternalServerError", model);
					// return View("Errors/NotFound", model);
					return View("Errors/NotFound");
			}
		}

		
	}
}
