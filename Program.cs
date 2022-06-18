using CompanionApp.Models;
using CompanionApp.Services;
using CompanionApp.Services.Contracts;
using CompanionApp.Validation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = "API Documentation";
    Console.WriteLine(xmlFilename);
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Configuration
    .AddJsonFile            ("appsettings.json")
    .AddUserSecrets<Program>(true)
    .AddEnvironmentVariables()
    .AddCommandLine         (args)
    .Build                  ();

#if DEBUG
builder.Services.AddDbContext<CompanionAppDBContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DevDB"]));
#else
builder.Services.AddDbContext<CompanionAppDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CompanionAppDB")));
#endif

builder.Services
    .AddScoped<IProfileService, ProfileService>()
    .AddScoped<ICourseService , CourseService >()
    .AddScoped<IPostService   , PostService   >();

builder.Services
    .AddScoped<ProfileValidation>()
    .AddScoped<CourseValidation >();

WebApplication app = builder.Build();

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
