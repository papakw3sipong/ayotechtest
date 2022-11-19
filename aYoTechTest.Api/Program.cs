using aYoTechTest.Api.Classes;
using aYoTechTest.BR.Classes;
using aYoTechTest.BR.Services.Interfaces;
using aYoTechTest.CommonLibraries.Helpers;
using aYoTechTest.CommonLibraries.Interfaces;
using aYoTechTest.DAL.Classes;
using aYoTechTest.DAL.SeedData;
using aYoTechTest.Models.Entities.Identity;
using aYoTechTest.Services.Classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

var apiSettingSection = configuration.GetSection("ApiSetting");



services.Configure<ApiSetting>(apiSettingSection);

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<IdentityUserContext>(options =>
  options.UseNpgsql(
      configuration.GetConnectionString("AyoDbConnection")));

services.AddDbContext<AppDataContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("AyoDbConnection"));
});

var _apiSettings = apiSettingSection.Get<ApiSetting>();

services.AddIdentity<aYoTechTestUser, IdentityRole>(options =>
{
    options.Stores.MaxLengthForKeys = 512;
    options.SignIn.RequireConfirmedEmail = false;

    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = _apiSettings.PasswordLength;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(_apiSettings.LockoutTimeInMinutes);
    options.Lockout.MaxFailedAccessAttempts = _apiSettings.AllowdFailedAttempts;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters = _apiSettings.AllowedUserNameChars;
}
).AddEntityFrameworkStores<IdentityUserContext>();


services.AddCors(o =>
{
    o.AddPolicy(ApiConstants.CORS_NAME, opt =>
    {
        opt.AllowAnyOrigin();
        opt.AllowAnyMethod();
        opt.AllowAnyHeader();
    });
});
services.AddControllers();
services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = _apiSettings.TokenIssuer,
        ValidAudience = _apiSettings.TokenAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.TokenSecret))
    };
});
services.AddAuthorization();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(ApiConstants.API_NAME, new OpenApiInfo
    {
        Title = $"{ApiConstants.API_NAME} - {ApiConstants.API_VERSION}",
        Contact = new OpenApiContact() { Name = "Charles Yeboah Oppong" },
        Description = ApiConstants.API_DESCRIPTION
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);

    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Description = "Basic authorization header. Example: \"Authorization: username:password\"",
        Name = "BasicAuthorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "basic"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme()
          {
            Reference = new OpenApiReference() {Type = ReferenceType.SecurityScheme, Id = "Basic"}
          },
          new[] {"readAccess", "writeAccess"}
        }
      });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme()
          {
            Reference = new OpenApiReference() {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
          },
          new[] {"readAccess", "writeAccess"}
        }});


    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
    xmlFiles.ToList().ForEach(f => c.IncludeXmlComments(f));
    c.DescribeAllParametersInCamelCase();
});





#if DEBUG
services.AddScoped<UnitConversionSeedData>();
services.AddScoped<DefaultUserSeedData>();
#endif

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddScoped<IUnitConvertionService, UnitConversionService>();
services.AddScoped<IIdentityUserService, IdentityUserService>();
services.AddScoped<ICurrentUserHelper, CurrentUserHelper>();




var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
#if DEBUG
    SeedData();
#endif

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApiConstants.API_NAME} - {ApiConstants.API_VERSION}");
        //s.RoutePrefix = String.Empty;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(ApiConstants.CORS_NAME);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


#if DEBUG
void SeedData()
{
    var _seedHelper = new SeedDataHelper(app.Services);
    _seedHelper.RunSeed();
}
#endif



