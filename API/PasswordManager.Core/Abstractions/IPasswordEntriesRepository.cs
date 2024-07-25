using PasswordManager.Core.Models;

namespace PasswordManager.DataAccess.Repositories
{
	public interface IPasswordEntriesRepository
	{
		Task<Guid> Create(PasswordEntry entry);
		Task<Guid> Delete(Guid id);
		Task<List<PasswordEntry>> Get();
		Task<Guid> Update(Guid id, string name, string password, bool isSite);
	}
}