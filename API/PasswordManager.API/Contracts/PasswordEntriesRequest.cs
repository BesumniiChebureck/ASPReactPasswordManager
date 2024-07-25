namespace PasswordManager.API.Contracts
{
	public record PasswordEntriesRequest(
		string Name,
		string Password,
		bool IsSite);
}
