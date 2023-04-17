using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Settings;
using KwiatkiBeatkiAPI.Services;

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

var app = builder.Build();

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
