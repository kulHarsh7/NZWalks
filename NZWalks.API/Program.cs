

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Data;
using NZWalks.API.Mapping;
using NZWalks.API.Middleware;
using NZWalks.API.Repositories.Implementation;
using NZWalks.API.Repositories.Interfaces;
using Serilog;
using System.Text;

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

builder.Services.AddDbContext<NZWalksAuthDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();

builder.Services.AddIdentityCore<IdentityUser>().
    AddRoles<IdentityRole>().
    AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks").
    AddEntityFrameworkStores<NZWalksAuthDBContext>().
    AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
