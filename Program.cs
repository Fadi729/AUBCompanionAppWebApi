using CompanionApp.Models;
using CompanionApp.Services;
using CompanionApp.Validation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using CompanionApp.Services.Contracts;
using CompanionApp.Validation.CommentValidation;
using CompanionApp.Exceptions.ExceptionMiddlewareNS;
using CompanionApp.Validation.PostValidation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Configuration
#region Configurations
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>(true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build(); 
#endregion

#if DEBUG
builder.Services.AddDbContext<CompanionAppDBContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DevDB"]));
#else
builder.Services.AddDbContext<CompanionAppDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CompanionAppDB")));
#endif

builder.Services
#region Services
    .AddScoped<IProfileService      , ProfileService      >()
    .AddScoped<ICourseService       , CourseService       >()
    .AddScoped<IPostService         , PostService         >()
    .AddScoped<ISemesterService     , SemesterService     >()
    .AddScoped<IFollowingsService   , FollowingsService   >()
    .AddScoped<ILikesService        , LikesService        >()
    .AddScoped<ICourseTakenByService, CourseTakenByService>()
    .AddScoped<ICommentsService     , CommentsService     >();
#endregion

builder.Services
#region Validations
    .AddScoped<ProfileValidation      >()
    .AddScoped<CourseValidation       >()
    .AddScoped<SemesterValidation     >()
    .AddScoped<CourseTakenByValidation>()
    .AddScoped<AddCommentValidation   >()
    .AddScoped<EditCommentValidation  >()
    .AddScoped<FollowingsValidation   >()
    .AddScoped<LikeValidation         >()
    .AddScoped<AddPostValidation      >()
    .AddScoped<EditPostValidation     >();
#endregion

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
