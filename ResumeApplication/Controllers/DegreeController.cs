using Microsoft.AspNetCore.Mvc;
using ResumeApplication.Entities;
using ResumeApplication.Interfaces;
using ResumeApplication.Models.ViewModels.Degree;

namespace ResumeApplication.Controllers
{
    public class DegreeController : Controller
	{

        /// <summary>
        /// The <see cref="IDegreeService"/>.
        /// </summary>
        private readonly IDegreeService _degreeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DegreeController"/> class.
        /// </summary>
        /// <param name="degreeService"> The <see cref="IDegreeService"/> </param>
        public DegreeController(IDegreeService degreeService)
        {
            _degreeService = degreeService;
        }

		public async Task<IActionResult> Index()
        {

            var viewModel = await _degreeService.GetDegreesAsync().ConfigureAwait(true);

            return View(viewModel);
		}


		[HttpGet("degrees/add")]
		public IActionResult AddDegree()
		{
			return View();
		}

		[HttpPost("degrees/add")]
		public async Task<IActionResult> AddDegree(AddDegreeViewModel addDegreeViewModel)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					// Add the degree to the database
					return RedirectToAction("Index");
				}

                var modelDegree = new Degree
                {
                    Name = addDegreeViewModel.Name
                };

				var model = await _degreeService.AddDegreeAsync(modelDegree).ConfigureAwait(true);

				return RedirectToAction("Index");

			}
			catch (Exception e)
			{

			}

			return null;
		}

	}
}
