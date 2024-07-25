namespace PasswordManager.API.Contracts
{
	public record PasswordEntriesResponse(
		Guid Id,
		string Name,
		string Password,
		DateTime EntryDate,
		bool IsSite);
}
