namespace PasswordManager.DataAccess.Entities
{
	public class PasswordEntryEntity
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		public DateTime EntryDate { get; set; } = DateTime.Now;

		public bool IsSite { get; set; } = true;
	}
}
