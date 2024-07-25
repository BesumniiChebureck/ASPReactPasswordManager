using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.Contracts;
using PasswordManager.Application.Services;
using PasswordManager.Core.Models;

namespace PasswordManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
	public class PasswordEntriesController : ControllerBase
	{
		private readonly IPasswordEntryService _entryService;

		public PasswordEntriesController(IPasswordEntryService entryService)
        {
			this._entryService = entryService;
		}

		[HttpGet]
		public async Task<ActionResult<List<PasswordEntriesResponse>>> GetPasswordEntries()
		{
			try
			{
				var entries = await _entryService.GetAllPasswordEntries();

				var response = entries.Select(e => new PasswordEntriesResponse(e.Id, e.Name, e.Password, e.EntryDate, e.IsSite));

				return Ok(response);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> CreatePasswordEntry([FromBody] PasswordEntriesRequest request)
		{
			try
			{
				var (entry, error) = PasswordEntry.Create(
				Guid.NewGuid(),
				request.Name,
				request.Password,
				DateTime.UtcNow,
				request.IsSite);

				var entries = await _entryService.GetAllPasswordEntries();

				if (entries.FirstOrDefault(e => e.Name.ToLower() == request.Name.ToLower()) != null)
					error = (error ?? "") + "Ресурс с таким названием уже существует.";

				if (!String.IsNullOrEmpty(error))
				{
					return BadRequest(new ErrorResponse(error));
				}

				var entryId = await _entryService.CreatePasswordEntry(entry);

				return Ok(entryId);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPut("{id:guid}")]
		public async Task<ActionResult<Guid>> UpdatePasswordEntry(Guid id, [FromBody] PasswordEntriesRequest request)
		{
			try
			{
				var error = PasswordEntry.Validate(
				request.Name,
				request.Password,
				request.IsSite);

				if (!String.IsNullOrEmpty(error))
				{
					return BadRequest(error);
				}

				var entryId = await _entryService.UpdatePasswordEntry(id, request.Name, request.Password, request.IsSite);

				return Ok(entryId);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpDelete("{id:guid}")]
		public async Task<ActionResult<Guid>> DeletePasswordEntry(Guid id)
		{
			try
			{
				return Ok(await _entryService.DeletePasswordEntry(id));
			}
			catch(Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
    }
}
