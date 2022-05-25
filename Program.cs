//using CompanionApp.Models;
//using CompanionApp.Models;
using CompanionApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>(true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

#if DEBUG
    builder.Services.AddDbContext<CompanionAppDBContext>(options =>
            options.UseSqlServer(builder.Configuration["ConnectionStrings:DevDB"]));

#else
    builder.Services.AddDbContext<CompanionAppDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CompanionAppDB")));
#endif


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

