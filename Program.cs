using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Models.Settings;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Identity;

var databaseInfo = new DatabaseInfo();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection("DatabaseInfo").Bind(databaseInfo);
builder.Services.AddSingleton(databaseInfo);

// Add services to the container.
builder.Services.AddTransient<ISeederService, SeederService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KwiatkiBeatkiDbContext>();
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeederService>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
