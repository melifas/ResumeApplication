using ResumeApplication.Context;
using ResumeApplication.Entities;
using ResumeApplication.Interfaces;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using ResumeApplication.Models;
using ResumeApplication.Helpers;
using System.Text;
using ResumeApplication.Models.ViewModels.Candidate;
using Microsoft.EntityFrameworkCore;

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
		public async Task<ReadOnlyCollection<Candidate>> GetCandidatesAsync()
		{

			var candidates = await _context.Candidates
				.Include(c => c.CandidateFile)
				.Include(c => c.Degree)
				.AsNoTracking().ToListAsync()
				.ConfigureAwait(false);

			return candidates.AsReadOnly();
		}

		/// <inheritdoc/>
		public async Task<Candidate> GetCandidateAsync(int candidateId)
		{

			var candidateModel = await _context.Candidates
				.Include(c => c.CandidateFile)
				.Include(c => c.Degree)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == candidateId)
				.ConfigureAwait(false);

			if (candidateModel == null)
			{
				throw new KeyNotFoundException($"Candidate with id {candidateId} does not exist");
			}

			return candidateModel;
		}

		/// <inheritdoc/>
		public async Task<int> AddCandidateAsync(AddCandidateViewModel candidateModel)
		{
			if (candidateModel == null)
			{
				throw new ArgumentNullException(nameof(candidateModel), "Supplied object was null");
			}

			Candidate candidate;

			if (candidateModel.FileInfo is not null)
			{
				var candidateFile = CreateCompanyServiceFiles(candidateModel.FileInfo);

				candidate = new Candidate
				{
					FirstName = candidateModel.FirstName,
					LastName = candidateModel.LastName,
					Email = candidateModel.Email,
					Mobile = candidateModel.Mobile,
					CreationTime = DateTime.UtcNow,
					DegreeId = candidateModel.DegreeId,
					CandidateFile = candidateFile
				};
			}
			else
			{
				candidate = new Candidate
				{
					FirstName = candidateModel.FirstName,
					LastName = candidateModel.LastName,
					Email = candidateModel.Email,
					Mobile = candidateModel.Mobile,
					CreationTime = DateTime.UtcNow,
					DegreeId = candidateModel.DegreeId
				};
			}

			await _context.Candidates.AddAsync(candidate).ConfigureAwait(false);

			//await _context.CandidateFiles.AddAsync(candidate.CandidateFile).ConfigureAwait(false);

			await _context.SaveChangesAsync().ConfigureAwait(false);

			return candidate.Id;

		}

		/// <inheritdoc/>
		public async Task UpdateCandidateAsync(EditCandidateViewModel editCandidateModel)
		{
			if (editCandidateModel == null)
			{
				throw new ArgumentNullException(nameof(editCandidateModel), "Supplied update object was null");
			}

			var candidateModel = await _context.Candidates
				.Include(c => c.CandidateFile)
				.Include(c => c.Degree)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == editCandidateModel.Id)
				.ConfigureAwait(false);

			if (candidateModel == null)
			{
				throw new KeyNotFoundException($"Candidate with id {editCandidateModel.Id}  does not exist");
			}

			if (editCandidateModel.FileInfo is not null)
			{
				
				if (candidateModel.CandidateFile is null)
				{
					//New file. Add it
					var candidateFile = CreateCompanyServiceFiles(editCandidateModel.FileInfo);

					candidateModel.CandidateFile = candidateFile;
				}
				else
				{
					//Existing file. Delete it and add the new one

					_context.CandidateFiles.Remove(candidateModel.CandidateFile);

					var candidateFile = CreateCompanyServiceFiles(editCandidateModel.FileInfo);

					candidateModel.CandidateFile = candidateFile;
				}

			}


			candidateModel.FirstName = editCandidateModel.FirstName;
			candidateModel.LastName = editCandidateModel.LastName;
			candidateModel.Email = editCandidateModel.Email;
			candidateModel.Mobile = editCandidateModel.Mobile;
			candidateModel.DegreeId = editCandidateModel.DegreeId;
			candidateModel.Degree = _context.Degrees.FirstOrDefault(x => x.Id == editCandidateModel.DegreeId);

			_context.Candidates.Update(candidateModel);

			await _context.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task DeleteCandidateAsync(int candidateId)
		{
			var candidateToDelete = await _context.Candidates.FirstOrDefaultAsync(a => a.Id == candidateId).ConfigureAwait(false);

			if (candidateToDelete == null)
			{
				throw new KeyNotFoundException($"Candidate with id {candidateId} does not exist.");
			}

			_context.Candidates.Remove(candidateToDelete);

			await _context.SaveChangesAsync().ConfigureAwait(false);
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
