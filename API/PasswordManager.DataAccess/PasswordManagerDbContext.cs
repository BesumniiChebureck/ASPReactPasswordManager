using Microsoft.EntityFrameworkCore;
using PasswordManager.DataAccess.Entities;

namespace PasswordManager.DataAccess
{
	public class PasswordManagerDbContext : DbContext
	{
		public PasswordManagerDbContext(DbContextOptions<PasswordManagerDbContext> options)
			: base(options)
		{
		}

		public DbSet<PasswordEntryEntity> PasswordEntries { get; set; }
	}
}
