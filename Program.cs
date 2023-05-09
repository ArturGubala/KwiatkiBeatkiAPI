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
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using KwiatkiBeatkiAPI.Models.Item;
using KwiatkiBeatkiAPI.Validators;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using KwiatkiBeatkiAPI.Models.Document;

var autenticationSettings = new AutenticationSettings();
var databaseInfo = new DatabaseInfo();
var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddFluentValidationRulesToSwagger();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAuthService, AuthorizeService>();
builder.Services.AddTransient<IItemsService, ItemsService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IItemTypesService, ItemTypesService>();
builder.Services.AddTransient<IBulkPacksService, BulkPacksService>();
builder.Services.AddTransient<IMeasurementUnitsService, MeasurementUnitsService>();
builder.Services.AddTransient<IProducersService, ProducersService>();
builder.Services.AddTransient<IDocumentsService, DocumentsService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IValidator<CreateItemDto>, CreateItemDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateItemDto>, UpdateItemDtoValidator>();
builder.Services.AddScoped<IValidator<CreateDocumentDto>, CreateDocumentDtoValidator>();
builder.Services.AddDbContext<KwiatkiBeatkiDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("KwiatkiBeatkiDbConnection")));

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
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KwiatkiBeatki API");
});
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
