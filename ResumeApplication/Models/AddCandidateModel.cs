using ResumeApplication.Models.ViewModels.Candidate;

namespace ResumeApplication.Models
{
	public class AddCandidateModel
	{
		public int DegreeId { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string Email { get; set; }

		public string Mobile { get; set; }

		public DateTime CreationTime { get; set; }

		public FileInfoViewModel FileInfo { get; set; }
	}
}
