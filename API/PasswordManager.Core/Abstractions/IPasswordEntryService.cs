using PasswordManager.Core.Models;

namespace PasswordManager.Application.Services
{
	public interface IPasswordEntryService
	{
		Task<Guid> CreatePasswordEntry(PasswordEntry entry);
		Task<Guid> DeletePasswordEntry(Guid id);
		Task<List<PasswordEntry>> GetAllPasswordEntries();
		Task<Guid> UpdatePasswordEntry(Guid id, string name, string password, bool isSite);
	}
}