using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Services;
using PasswordManager.DataAccess;
using PasswordManager.DataAccess.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PasswordManagerDbContext>(
	options =>
	{
		options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(PasswordManagerDbContext)));
	});

builder.Services.AddScoped<IPasswordEntryService, PasswordEntryService>();
builder.Services.AddScoped<IPasswordEntriesRepository, PasswordEntriesRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
{
	x.WithHeaders().AllowAnyHeader();
	x.WithOrigins().AllowAnyOrigin();
	x.WithMethods().AllowAnyMethod();
});

app.Run();
