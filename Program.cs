using KwiatkiBeatkiAPI.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

var databaseInfo = new DatabaseInfo();
builder.Configuration.GetSection("DatabaseInfo").Bind(databaseInfo);
builder.Services.AddSingleton(databaseInfo);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
