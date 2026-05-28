using FluentValidation;
using LoonyBin.API.Dtos.Validators;
using LoonyBin.API.Middlewares;
using LoonyBin.Services.Patients;
using LoonyBin.Infrastructure;
using LoonyBin.Services.DateFilters;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});
builder.Services.AddOpenApi();

builder.Services.AddHostedService<MigrationHostedService>();
builder.Services.AddScoped<EntitySaveChangesInterceptor>();

builder.Services.AddDbContext<PatientDbContext>((serviceProvider, options) => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddInterceptors(serviceProvider.GetRequiredService<EntitySaveChangesInterceptor>()));

builder.Services.AddValidatorsFromAssemblyContaining<PatientRequestValidator>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDateSearchService, DateSearchService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
