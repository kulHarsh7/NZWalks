

using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mapping;
using NZWalks.API.Middleware;
using NZWalks.API.Repositories.Implementation;
using NZWalks.API.Repositories.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .WriteTo.File("Logs/NZWalks_Logs.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalksDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZwalksConnectionString")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
