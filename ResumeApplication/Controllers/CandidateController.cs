using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeApplication.Entities;
using ResumeApplication.Interfaces;
using ResumeApplication.Models.ViewModels.Candidate;
using System.Drawing;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateController"/> class.
        /// </summary>
        /// <param name="candidateService"> The <see cref="ICandidateService"/>. </param>
        public CandidateController(ICandidateService candidateService, IDegreeService degreeService)
        {
            _candidateService = candidateService;
            _degreeService = degreeService;
        }

        [HttpGet("candidates")]
        public IActionResult Index()
        {


            return View();
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
                    return View(addCandidateViewModel);
                }


				var addCandidate = new AddCandidateModel
				{
					DegreeId = addCandidateViewModel.DegreeId,
					LastName = addCandidateViewModel.LastName,
					FirstName = addCandidateViewModel.FirstName,
					Email = addCandidateViewModel.Email,
					Mobile = addCandidateViewModel.Moblile,
                    FileInfo = new FileInfoViewModel
                    {
	                    EncodedFile = FileConverter.ConvertToBase64(addCandidateViewModel.UploadCv),
	                    FileName = addCandidateViewModel.UploadCv.FileName
                    }
				};

		        await _candidateService.AddCandidateAsync(addCandidate).ConfigureAwait(true);

		        return RedirectToAction("Index");

	        }
	        catch (Exception e)
	        {

	        }

	        return null;
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
