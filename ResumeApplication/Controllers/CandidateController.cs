using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeApplication.Interfaces;
using ResumeApplication.Models.ViewModels.Candidate;
using ResumeApplication.Helpers;
using ResumeApplication.Models;

namespace ResumeApplication.Controllers
{
	public class CandidateController : Controller
	{
		/// <summary>
		/// The <see cref="ICandidateService"/>
		/// </summary>
		private readonly ICandidateService _candidateService;

		/// <summary>
		/// The <see cref="IDegreeService"/>
		/// </summary>
		private readonly IDegreeService _degreeService;

		//private readonly IValidator<AddCandidateViewModel> _validator;

		/// <summary>
		/// Initializes a new instance of the <see cref="CandidateController"/> class.
		/// </summary>
		/// <param name="candidateService"> The <see cref="ICandidateService"/>. </param>
		/// <param name="degreeService"> The <see cref="IDegreeService"/>. </param>
		/// <param name="validator"></param>
		public CandidateController(ICandidateService candidateService, IDegreeService degreeService /*IValidator<AddCandidateViewModel> validator*/)
		{
			_candidateService = candidateService;
			_degreeService = degreeService;
			//_validator = validator;
		}

		[HttpGet("candidates")]
		public async Task<IActionResult> Index()
		{
			var viewModel = await _candidateService.GetCandidatesAsync().ConfigureAwait(true);

			return View(viewModel);
		}

		[HttpGet("candidates/add")]
		public async Task<IActionResult> AddCandidate()
		{
			var degrees = await GetDegreesAsync().ConfigureAwait(true);

			var viewModel = new AddCandidateViewModel
			{
				Degrees = degrees
			};

			return View(viewModel);
		}

		[HttpPost("candidates/add")]
		public async Task<IActionResult> AddCandidate(AddCandidateViewModel addCandidateViewModel)
		{

			try
			{

				if (!ModelState.IsValid)
				{

					addCandidateViewModel.Degrees = await GetDegreesAsync().ConfigureAwait(true);

					return View(addCandidateViewModel);
				}

				var addCandidate = new AddCandidateViewModel
				{
					DegreeId = addCandidateViewModel.DegreeId ?? null,
					LastName = addCandidateViewModel.LastName,
					FirstName = addCandidateViewModel.FirstName,
					Email = addCandidateViewModel.Email,
					Mobile = addCandidateViewModel.Mobile,
				};

				if (addCandidateViewModel.UploadCv is not null)
					addCandidate.FileInfo = new FileInfoViewModel
					{
						EncodedFile = FileConverter.ConvertToBase64(addCandidateViewModel.UploadCv),
						FileName = addCandidateViewModel.UploadCv.FileName
					};

				await _candidateService.AddCandidateAsync(addCandidate).ConfigureAwait(true);

				return RedirectToAction("Index");

			}
			catch (KeyNotFoundException ke)
			{
				//logging here
				return View("Errors/NotFound");
			}
			catch (Exception e)
			{
				return View("Errors/InternalServerError");
			}
		}


		[HttpGet("candidates/{candidateId:int}/edit")]
		public async Task<IActionResult> EditCandidate(int candidateId)
		{
			var candidate = await _candidateService.GetCandidateAsync(candidateId).ConfigureAwait(true);

			if (candidate == null)
			{
				return NotFound();
			}

			var editCandidateModel = new EditCandidateViewModel
			{
				Id = candidate.Id,
				FirstName = candidate.FirstName,
				LastName = candidate.LastName,
				Email = candidate.Email,
				Mobile = candidate.Mobile,
				DegreeId = candidate.DegreeId,
				Degrees = await GetDegreesAsync().ConfigureAwait(true)
			};

			return View(editCandidateModel);

		}

		[HttpPost("candidates/{candidateId:int}/edit")]
		public async Task<IActionResult> EditCandidate(EditCandidateViewModel editCandidateViewModel)
		{

			try
			{

				if (!ModelState.IsValid)
				{

					editCandidateViewModel.Degrees = await GetDegreesAsync().ConfigureAwait(true);

					return View(editCandidateViewModel);
				}

				var modelDegree = new EditCandidateViewModel
				{
					Id = editCandidateViewModel.Id,
					FirstName = editCandidateViewModel.FirstName,
					LastName = editCandidateViewModel.LastName,
					Email = editCandidateViewModel.Email,
					Mobile = editCandidateViewModel.Mobile,
					DegreeId = editCandidateViewModel.DegreeId??null
				};

				if (editCandidateViewModel.UploadCv is not null)
					modelDegree.FileInfo = new FileInfoViewModel
					{
						EncodedFile = FileConverter.ConvertToBase64(editCandidateViewModel.UploadCv),
						FileName = editCandidateViewModel.UploadCv.FileName
					};

				await _candidateService.UpdateCandidateAsync(modelDegree).ConfigureAwait(true);

				return RedirectToAction("Index");
			}
			catch (KeyNotFoundException ke)
			{
				//logging here
				return View("Errors/NotFound");
			}
			catch (Exception e)
			{
				return View("Errors/InternalServerError");
			}
		}

		[HttpPost("candidates/{candidateId:int}/delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteCandidate(int candidateId)
		{

			try
			{
				await _candidateService.DeleteCandidateAsync(candidateId).ConfigureAwait(true);

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

		private async Task<List<SelectListItem>> GetDegreesAsync()
		{
			var degreeResults = await _degreeService.GetDegreesAsync().ConfigureAwait(true);

			var degrees = new List<SelectListItem>();

			degrees.AddRange(degreeResults.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Name
			}));

			return degrees;
		}

	}
}
