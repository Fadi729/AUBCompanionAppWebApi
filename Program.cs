using System.Text;
using CompanionApp.Jwt;
using CompanionApp.Models;
using CompanionApp.Services;
using CompanionApp.Validation;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using CompanionApp.Services.Contracts;
using CompanionApp.Validation.PostValidation;
using CompanionApp.Validation.CommentValidation;
using CompanionApp.Exceptions.ExceptionMiddlewareNS;
using Microsoft.AspNetCore.Authentication.JwtBearer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(x =>
    {
        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In          = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name        = "Authorization",
            Type        = SecuritySchemeType.ApiKey
        }); 
        x.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id   = "Bearer"
                    },
                    In        = ParameterLocation.Header,
                    Name      = "Bearer",

                },
                new List<string>()
            }
        });
    });

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
builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<CompanionAppDBContext>();
#else
builder.Services.AddDbContext<CompanionAppDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CompanionAppDB")));
builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<CompanionAppDBContext>();
#endif

builder.Services.AddScoped<JwtSettings>();

builder.Services
#region Services
    .AddScoped<IUserManager         , UserManager         >()
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

JwtSettings jwtSettings = new();
builder.Configuration.Bind(nameof(JwtSettings), jwtSettings) ;
builder.Services.AddSingleton(jwtSettings) ;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme             = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwt =>
    {
        jwt.SaveToken                 = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
            ValidateIssuer           = false,
            ValidateAudience         = false,
            RequireExpirationTime    = false,
            ValidateLifetime         = true,
        };
    });

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
