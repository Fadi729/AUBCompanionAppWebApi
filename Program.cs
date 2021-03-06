using CompanionApp.Exceptions;
using CompanionApp.Exceptions.ExceptionMiddlewareNS;
using CompanionApp.Jwt;
using CompanionApp.Models;
using CompanionApp.Models.Identity_models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using CompanionApp.Services;
using CompanionApp.Services.Contracts;
using CompanionApp.Validation;

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
builder.Services.AddIdentity<Profile, AppRole>(options =>
{
    options.Password.RequireDigit           = false;
    options.Password.RequireLowercase       = false;
    options.Password.RequireUppercase       = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength         = 0;
    options.Password.RequiredUniqueChars    = 0;
}).AddEntityFrameworkStores<CompanionAppDBContext>();
#else
builder.Services.AddDbContext<CompanionAppDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CompanionAppDB")));
builder.Services.AddIdentity<Profile, AppRole>(options =>
{
    options.Password.RequireDigit           = false;
    options.Password.RequireLowercase       = false;
    options.Password.RequireUppercase       = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength         = 0;
    options.Password.RequiredUniqueChars    = 0;
}).AddEntityFrameworkStores<CompanionAppDBContext>();
#endif

builder.Services.AddScoped<JwtSettings>();

builder.Services
#region Services
    .AddScoped<IUserService         , UserService         >()
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
    .AddScoped<ProfileRegistrationValidation>()
    .AddScoped<CourseValidation             >()
    .AddScoped<SemesterValidation           >()
    .AddScoped<CourseTakenByValidation      >();
#endregion

JwtSettings jwtSettings = new();
builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);
builder.Services.AddSingleton(jwtSettings);

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
        jwt.Events = new JwtBearerEvents
        {
            OnChallenge = async _ => throw new UnauthorizedRequestException()
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

app.MapControllers();

app.UseAuthorization();

app.Run();
