using PasswordManager.Core.Models;
using PasswordManager.DataAccess.Repositories;
using System.Collections.Generic;

namespace PasswordManager.Application.Services
{
	public class PasswordEntryService : IPasswordEntryService
	{
		private readonly IPasswordEntriesRepository _entriesRepository;

		public PasswordEntryService(IPasswordEntriesRepository entriesRepository)
		{
			this._entriesRepository = entriesRepository;
		}

		public async Task<List<PasswordEntry>> GetAllPasswordEntries()
		{
			return await _entriesRepository.Get();
		}

		public async Task<Guid> CreatePasswordEntry(PasswordEntry entry)
		{
			return await _entriesRepository.Create(entry);
		}

		public async Task<Guid> UpdatePasswordEntry(Guid id, string name, string password, bool isSite)
		{
			return await _entriesRepository.Update(id, name, password, isSite);
		}

		public async Task<Guid> DeletePasswordEntry(Guid id)
		{
			return await _entriesRepository.Delete(id);
		}
	}
}
