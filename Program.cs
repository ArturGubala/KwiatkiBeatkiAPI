using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Middleware;
using KwiatkiBeatkiAPI.Models.Settings;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Identity;
using KwiatkiBeatkiAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using KwiatkiBeatkiAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;
using NLog.Web;
using QuestPDF.Infrastructure;

try
{
    var autenticationSettings = new AutenticationSettings();
    var databaseInfo = new DatabaseInfo();
    var builder = WebApplication.CreateBuilder(args);
    QuestPDF.Settings.License = LicenseType.Community;

    builder.Configuration.GetSection("Autentication").Bind(autenticationSettings);
    builder.Services.AddSingleton(autenticationSettings);

    var config = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();


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
    builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var validationErrors = context.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

                var response = new ValidationErrorResponse
                {
                    Message = "Validation errors occurred.",
                    Errors = validationErrors
                };

                return new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            };
        });

    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    });

    builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    builder.Services.AddFluentValidationRulesToSwagger();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<IUserContextService, UserContextService>();

    builder.Services.AddTransient<ITokenService, TokenService>();
    builder.Services.AddTransient<IAuthService, AuthorizeService>();
    builder.Services.AddTransient<IItemsService, ItemsService>();
    builder.Services.AddTransient<IUsersService, UsersService>();
    builder.Services.AddTransient<IItemTypesService, ItemTypesService>();
    builder.Services.AddTransient<IBulkPacksService, BulkPacksService>();
    builder.Services.AddTransient<IMeasurementUnitsService, MeasurementUnitsService>();
    builder.Services.AddTransient<IProducersService, ProducersService>();
    builder.Services.AddTransient<IDocumentsService, DocumentsService>();
    builder.Services.AddTransient<IItemPropertiesService, ItemPropertiesService>();
    builder.Services.AddTransient<IDocumentTypeService, DocumentTypeService>();

    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

    builder.Services.AddDbContext<KwiatkiBeatkiDbContext>
        (options => options.UseSqlServer(builder.Configuration.GetConnectionString("KwiatkiBeatkiDbConnection")));

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
    builder.Host.UseNLog();

    var app = builder.Build();
    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<ISeederService>();
    seeder.Seed();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI();
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

}
catch (Exception)
{
    throw;
}
