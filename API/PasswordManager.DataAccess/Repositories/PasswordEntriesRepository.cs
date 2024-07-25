using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Models;
using PasswordManager.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccess.Repositories
{
	public class PasswordEntriesRepository : IPasswordEntriesRepository
	{
		private readonly PasswordManagerDbContext _context;

		public PasswordEntriesRepository(PasswordManagerDbContext context)
		{
			_context = context;
		}

		public async Task<List<PasswordEntry>> Get()
		{
			var entriesEntites = await _context.PasswordEntries
				.AsNoTracking()
				.ToListAsync();

			var entries = entriesEntites
				.Select(e => PasswordEntry.Create(e.Id, e.Name, e.Password, e.EntryDate, e.IsSite).PasswordEntry)
				.ToList();

			return entries;
		}

		public async Task<Guid> Create(PasswordEntry entry)
		{
			var entryEntity = new PasswordEntryEntity
			{
				Id = entry.Id,
				Name = entry.Name,
				Password = entry.Password,
				EntryDate = entry.EntryDate,
				IsSite = entry.IsSite
			};

			await _context.PasswordEntries.AddAsync(entryEntity);
			await _context.SaveChangesAsync();

			return entryEntity.Id;
		}

		public async Task<Guid> Update(Guid id, string name, string password, bool isSite)
		{
			await _context.PasswordEntries
				.Where(e => e.Id == id)
				.ExecuteUpdateAsync(s => s
					.SetProperty(e => e.Name, e => name)
					.SetProperty(e => e.Password, e => password)
					.SetProperty(e => e.IsSite, e => isSite));

			return id;
		}

		public async Task<Guid> Delete(Guid id)
		{
			await _context.PasswordEntries
				.Where(e => e.Id == id)
				.ExecuteDeleteAsync();

			return id;
		}
	}
}
