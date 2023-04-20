using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Middleware;
using KwiatkiBeatkiAPI.Models.Settings;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Identity;
using NLog;
using NLog.Extensions.Logging;
using KwiatkiBeatkiAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;

var autenticationSettings = new AutenticationSettings();
var databaseInfo = new DatabaseInfo();
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection("DatabaseInfo").Bind(databaseInfo);
builder.Services.AddSingleton(databaseInfo);
builder.Configuration.GetSection("Autentication").Bind(autenticationSettings);
builder.Services.AddSingleton(autenticationSettings);

var config = new ConfigurationBuilder()
   .SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
   .Build();
LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = autenticationSettings.JwtIssuer,
        ValidAudience = autenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(autenticationSettings.JwtKey))
    };
});

// Add services to the container.
builder.Services.AddTransient<ISeederService, SeederService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KwiatkiBeatkiDbContext>();
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAuthService, AuthorizeService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeederService>();
seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
