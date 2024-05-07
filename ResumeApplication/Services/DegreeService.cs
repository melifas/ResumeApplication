using Microsoft.EntityFrameworkCore;
using ResumeApplication.Context;
using ResumeApplication.Entities;
using ResumeApplication.Interfaces;
using ResumeApplication.Models.ServiceModels.Degree;
using System.Collections.ObjectModel;

namespace ResumeApplication.Services
{
	/// <summary>
	/// Implements the <see cref="IDegreeService"/> <see langword="interface"/>
	/// </summary>
	public class DegreeService : IDegreeService
	{
		/// <summary>
		/// Gets the <see cref="AppDbContext"/>.
		/// </summary>
		private readonly AppDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="CandidateService"/> class.
		/// </summary>
		/// <param name="context"> The <see cref="AppDbContext"/> </param>
		public DegreeService(AppDbContext context)
		{
			_context = context;
		}

		/// <inheritdoc/>
		public async Task<ReadOnlyCollection<Degree>> GetDegreesAsync()
		{
			var degrees = await _context.Degrees.AsNoTracking().ToListAsync().ConfigureAwait(false);

			return degrees.AsReadOnly();
		}

		/// <inheritdoc/>
		public async Task<GetDegreeResponse> GetDegreeAsync(int degreeId)
		{

			var degreeModel = await _context.Degrees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == degreeId)
				.ConfigureAwait(false);

			if (degreeModel == null)
			{
				throw new KeyNotFoundException($"Degree with id {degreeId} does not exist");
			}

			var response = new GetDegreeResponse
			{
				Id = degreeModel.Id,
				Name = degreeModel.Name
			};

			return response;
		}

		/// <inheritdoc/>
		public async Task<int> AddDegreeAsync(Degree degreeModel)
		{
			if (degreeModel == null)
			{
				throw new ArgumentNullException(nameof(degreeModel), "Supplied model was null");
			}

			var degree = new Degree { Name = degreeModel.Name, CreationTime = DateTime.Now };

			try
			{
				await _context.Degrees.AddAsync(degree);

				await _context.SaveChangesAsync().ConfigureAwait(false);
			}
			catch (Exception e)
			{
				//logging here..

				throw;
			}

			return degree.Id;
		}

		/// <inheritdoc/>
		public async Task UpdateDegreeAsync(Degree degreeModel)
		{

			if (degreeModel == null)
			{
				throw new ArgumentNullException(nameof(degreeModel), "Supplied model was null");
			}

			var degree = await _context.Degrees.FirstOrDefaultAsync(x => x.Id == degreeModel.Id).ConfigureAwait(false);

			if (degree == null)
			{
				throw new KeyNotFoundException($"Degree with id {degreeModel.Id} does not exist");
			}

			degree.Name = degreeModel.Name;

			try
			{
				_context.Degrees.Update(degree);

				await _context.SaveChangesAsync().ConfigureAwait(false);
			}
			catch (Exception e)
			{
				//logging here..

				throw;
			}
		}

		/// <inheritdoc/>
		public async Task DeleteDegreeAsync(int degreeId)
		{

			var degreeToDelete = await _context.Degrees.FirstOrDefaultAsync(x => x.Id == degreeId).ConfigureAwait(false);

			if (degreeToDelete == null)
			{
				throw new KeyNotFoundException($"Degree with id {degreeId} does not exist");
			}

			try
			{
				_context.Degrees.Remove(degreeToDelete);

				await _context.SaveChangesAsync().ConfigureAwait(false);
			}
			catch (Exception e)
			{
				//logging here..

				throw;
			}
		}

		/// <inheritdoc/>
		public async Task DeleteUnUsedAsync()
		{
			var used = await _context.Candidates.Where(c => c.DegreeId != null).ToListAsync().ConfigureAwait(false);

			var unusedDegrees = await _context.Degrees.Where(d => !used.Select(u => u.DegreeId).Contains(d.Id)).ToListAsync().ConfigureAwait(false);

			_context.Degrees.RemoveRange(unusedDegrees);

			await _context.SaveChangesAsync().ConfigureAwait(false);
		}


	}
}
