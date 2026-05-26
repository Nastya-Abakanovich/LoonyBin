using FluentValidation;
using LoonyBin.API.Middlewares;
using LoonyBin.DAL;
using LoonyBin.Features.DateFilters;
using LoonyBin.Features.Patients;
using LoonyBin.Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHostedService<MigrationHostedService>();
builder.Services.AddScoped<EntitySaveChangesInterceptor>();

builder.Services.AddDbContext<PatientDbContext>((serviceProvider, options) => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddInterceptors(serviceProvider.GetRequiredService<EntitySaveChangesInterceptor>()));
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IValidator<List<string>>, DateQueryValidator>();
builder.Services.AddScoped<IValidator<PatientRequest>, PatientRequestValidator>();

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
