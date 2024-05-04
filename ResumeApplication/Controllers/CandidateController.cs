using Microsoft.AspNetCore.Mvc;
using ResumeApplication.Interfaces;

namespace ResumeApplication.Controllers
{
    public class CandidateController : Controller
    {

        private readonly ICandidateService _candidateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateController"/> class.
        /// </summary>
        /// <param name="candidateService"> The <see cref="ICandidateService"/>. </param>
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet("candidates")]
        public IActionResult Index()
        {


            return View();
        }

        [HttpGet("candidates/add")]
        public IActionResult AddCandidate()
        {
	        return View();
        }

	}
}
