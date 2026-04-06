using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniClinicManagementSystem.API.Handlers;
using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Validations;
using MiniClinicManagementSystem.Infrastructure.Data;
using MiniClinicManagementSystem.Infrastructure.DependencyInjection;
using MiniClinicManagementSystem.Services.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection")
		)
	);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 6;
	options.Password.RequireNonAlphanumeric = false;
})
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		builder => builder
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());

});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAvailabilitySlotValidator>();
// Register Services and Repositories
builder.Services.AddServices();
builder.Services.AddRepositories();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();