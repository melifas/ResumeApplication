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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddDegree(AddDegreeViewModel addDegreeViewModel)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(addDegreeViewModel);
				}

				var modelDegree = new Degree
				{
					Name = addDegreeViewModel.Name
				};

				await _degreeService.AddDegreeAsync(modelDegree).ConfigureAwait(true);

				return RedirectToAction("Index");

			}
			catch (KeyNotFoundException ke)
			{
				return View("Errors/NotFound");
			}
			catch (Exception e)
			{
				return View("Errors/InternalServerError");
			}
		}

		[HttpGet("degrees/{degreeId:int}/edit")]
		public async Task<IActionResult> EditDegree(int degreeId)
		{

			try
			{
				var degree = await _degreeService.GetDegreeAsync(degreeId).ConfigureAwait(true);

				if (degree == null)
				{
					return NotFound();
				}

				var viewModel = new EditDegreeViewModel
				{
					Id = degree.Id,
					Name = degree.Name
				};

				return View(viewModel);
			}
			catch (KeyNotFoundException ke)
			{
				return View("Errors/NotFound");
			}
			catch (Exception e)
			{
				return View("Errors/InternalServerError");
			}

		}

		[HttpPost("degrees/{degreeId:int}/edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditDegree(EditDegreeViewModel editDegreeViewModel)
		{

			try
			{
				if (!ModelState.IsValid)
				{
					return View(editDegreeViewModel);
				}

				var modelDegree = new Degree
				{
					Id = editDegreeViewModel.Id,
					Name = editDegreeViewModel.Name
				};

				await _degreeService.UpdateDegreeAsync(modelDegree).ConfigureAwait(true);

				return RedirectToAction("Index");
			}
			catch (KeyNotFoundException ke)
			{
				return View("Errors/NotFound");
			}
			catch (Exception e)
			{
				return View("Errors/InternalServerError");
			}

		}

		[HttpPost("degrees/delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteDegree([FromBody] int degreeId)
		{

			try
			{
				await _degreeService.DeleteDegreeAsync(degreeId).ConfigureAwait(true);

				return RedirectToAction("Index");
			}
			catch (KeyNotFoundException ke)
			{
				throw;
			}
			catch (Exception e)
			{
				throw;
			}

		}


		[HttpPost("degrees/unused/delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteUnused(int degreeId)
		{
			try
			{
				await _degreeService.DeleteUnUsedAsync().ConfigureAwait(true);

				return RedirectToAction("Index");
			}
			catch (KeyNotFoundException ke)
			{
				return View("Errors/NotFound");
			}
			catch (Exception e)
			{
				return View("Errors/InternalServerError");
			}
		}


	}
}
