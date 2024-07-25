using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.DataAccess.Entities;

namespace PasswordManager.DataAccess.Configurations
{
	public class PasswordEntryConfiguration : IEntityTypeConfiguration<PasswordEntryEntity>
	{
		public void Configure(EntityTypeBuilder<PasswordEntryEntity> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(b => b.Name)
				.IsRequired();

			builder.Property(b => b.Password)
				.IsRequired();

			builder.Property(b => b.EntryDate)
				.IsRequired();

			builder.Property(b => b.IsSite)
				.IsRequired();
		}
	}
}
