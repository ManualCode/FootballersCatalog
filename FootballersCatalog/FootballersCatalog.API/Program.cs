using FootballersCatalog.API.Hubs;
using FootballersCatalog.Application.Services;
using FootballersCatalog.DataAcces;
using FootballersCatalog.DataAcces.Repositories;
using FootballersCatalog.Domain.Abstractions.Repositories;
using FootballersCatalog.Domain.Abstractions.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddDbContext<FootballersCatalogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FootballerPortal"))
    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddScoped<IFootballerService, FootballerService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IFootballerRepository, FootballerRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

var app = builder.Build();

app.MapHub<FootballersHub>("/football");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
