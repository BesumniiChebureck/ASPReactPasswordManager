using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Models
{
	public class PasswordEntry
	{
		public const int MIN_PASSWORD_LENGTH = 8;

		private PasswordEntry(Guid id, string name, string password, DateTime entryDate, bool isSite)
		{
			Id = id;
			Name = name;
			Password = password;
			EntryDate = entryDate;
			IsSite = isSite;
		}

		public Guid Id { get; }

		public string Name { get; } = string.Empty;

		public string Password { get; } = string.Empty;

		public DateTime EntryDate { get; } = DateTime.Now;

		public bool IsSite { get; } = true;

		/// <summary>
		/// Метод создания экземпляра класса PasswordEntry с валидацией полей
		/// </summary>
		/// <returns>Кортеж с созданным экземпляром класса PasswordEntry и результатом валидации</returns>
		public static (PasswordEntry PasswordEntry, string Error) Create(Guid id, string name, string password, DateTime entryDate, bool isSite)
		{
			var entry = new PasswordEntry(id, name, password, entryDate, isSite);

			return (entry, Validate(name, password, isSite));
		}

		/// <summary>
		/// Метод для валидации полей при создании или обновлении PasswordEntry
		/// </summary>
		/// <returns>Сообщение с ошибкой (если ошибок нет - пустое значение)</returns>
		public static string Validate(string name, string password, bool isSite)
		{
			StringBuilder error = new StringBuilder();

			if (String.IsNullOrWhiteSpace(name))
			{
				error.AppendLine("Введите название.");
			}

			if (String.IsNullOrWhiteSpace(password))
			{
				error.AppendLine("Введите пароль.");
			}
			else if (password.Length < MIN_PASSWORD_LENGTH)
			{
				error.AppendLine("Пароль должен быть минимум или больше 8 символов.");
			}

			if (!isSite)
			{
				if (!IsValidEmail(name))
				{
					error.AppendLine("Электронная почта введена в неверном формате.");
				}
			}

			return error.ToString();
		}

		/// <summary>
		/// Метод валидации email
		/// </summary>
		public static bool IsValidEmail(string email)
		{
			if (!MailAddress.TryCreate(email, out var mailAddress))
				return false;

			var hostParts = mailAddress.Host.Split('.');
			if (hostParts.Length == 1)
				return false;
			if (hostParts.Any(p => p == string.Empty))
				return false;
			if (hostParts[^1].Length < 2)
				return false;

			if (mailAddress.User.Contains(' '))
				return false;
			if (mailAddress.User.Split('.').Any(p => p == string.Empty))
				return false;

			return true;
		}
	}
}
