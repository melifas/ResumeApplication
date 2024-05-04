using ResumeApplication.Context;
using ResumeApplication.Entities;
using ResumeApplication.Interfaces;
using System.Collections.ObjectModel;
using ResumeApplication.Models;
using ResumeApplication.Helpers;
using System.Text;
using ResumeApplication.Models.ViewModels.Candidate;

namespace ResumeApplication.Services
{
	/// <summary>
	/// Implements the <see cref="ICandidateService"/> <see langword="interface"/>
	/// </summary>
	public class CandidateService : ICandidateService
	{
		/// <summary>
		/// Gets the <see cref="AppDbContext"/>.
		/// </summary>
		private readonly AppDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="CandidateService"/> class.
		/// </summary>
		/// <param name="context"> The <see cref="AppDbContext"/> </param>
		public CandidateService(AppDbContext context)
		{
			_context = context;
		}

		/// <inheritdoc/>
		public async Task<int> AddCandidateAsync(AddCandidateModel candidateModel)
		{
			if (candidateModel == null)
			{
				throw new ArgumentNullException(nameof(candidateModel), "Supplied object was null");
			}

			var candidateFile =CreateCompanyServiceFiles(candidateModel.FileInfo);

			var candidate = new Candidate
			{
				FirstName = candidateModel.FirstName,
				LastName = candidateModel.LastName,
				Email = candidateModel.Email,
				Mobile = candidateModel.Mobile,
				CreationTime = DateTime.UtcNow,
				DegreeId = candidateModel.DegreeId,
				CandidateFile = candidateFile
			};

			await _context.Candidates.AddAsync(candidate).ConfigureAwait(true);

			await _context.SaveChangesAsync().ConfigureAwait(false);

			return candidate.Id;

		}

		/// <inheritdoc/>
		public Task<ReadOnlyCollection<Candidate>> GetCandidatesAsync()
		{
			throw new NotImplementedException();
		}





		private static CandidateFile CreateCompanyServiceFiles(FileInfoViewModel fileInfo)
		{
			var now = DateTime.UtcNow;

			var fileToAdd = new CandidateFile
			{
				Name = string.Join(".", fileInfo.FileName.Split('.').SkipLast(1)),
				DataFile = Convert.FromBase64String(fileInfo.EncodedFile)
			};

			if (FileValidator.IsValidPdf(fileToAdd.DataFile))
			{
				fileToAdd.FileType = MimeTypes.PdfExtension;
			}
			else if (FileValidator.IsValidDocx(fileToAdd.DataFile))
			{
				fileToAdd.FileType = MimeTypes.DocxExtension;
			}
			else
			{
				var sb = new StringBuilder();
				sb.Append($"Unsupported file format, only {MimeTypes.PdfExtension.ToUpper()} ({MimeTypes.ApplicationPdf}), ");
				sb.Append($"{MimeTypes.DocxExtension.ToUpper()} ({MimeTypes.ApplicationWordOpenXml}), ");
				throw new NotSupportedException(sb.ToString());
			}

			return fileToAdd;
		}

	}
}
