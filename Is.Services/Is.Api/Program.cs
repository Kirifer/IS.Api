using System.Reflection;
using System.Text;

using Is.Api.Configurations;
using Is.Core.Abstraction;
using Is.Core.Api;
using Is.Core.ApiConfig;
using Is.Core.Authentication;
using Is.Core.Config;
using Is.Datalayer.Implementation;
using Is.Datalayer.Interface;
using Is.Shared.Constant;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});
// Setup environment variables initially
// builder.Configuration.AddUserSecrets<Program>();

// Setup configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var secretsConfig = builder.Configuration.GetSection("MicroServiceConfig")
    .Get<MicroServiceConfig>(c => c.BindNonPublicProperties = true);
if (secretsConfig == null)
{
    throw new Exception("MicroServiceConfig is not configured correctly");
}

builder.Services.AddSingleton<IMicroServiceConfig, MicroServiceConfig>(service => secretsConfig);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCoreLogging();
builder.Services.AddCoreControllers();
//builder.Services.AddControllers();
//builder.Services.AddCoreCompression();
builder.Services.AddCoreEntityServices<IEntityService>("Is.Domain");
builder.Services.AddAtsDatabase(secretsConfig);
builder.Services.AddScoped<ISuppliesRepository, SuppliesRepository>();
builder.Services.AddScoped<ISupplyCodesRepository, SupplyCodesRepository>();
builder.Services.AddScoped<IBudgetExpensesRepository, BudgetExpensesRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(GlobalConstant.DocumentVersion, new OpenApiInfo
    {
        Version = GlobalConstant.DocumentVersion,
        Title = GlobalConstant.DocumentTitle,
        Description = GlobalConstant.DocumentDescription
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Authentication Setup
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireSignedTokens = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretsConfig.JwtConfig!.Key)),

        ValidateLifetime = true,
        RequireExpirationTime = true,

        ValidateIssuer = true,
        ValidIssuer = secretsConfig.JwtConfig.Issuer,

        ValidateAudience = true,
        ValidAudience = secretsConfig.JwtConfig.Audience,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAtsDatabase();
app.UseRouting();
app.MapControllers();
// Use the CORS policy
app.UseCors("AllowSpecificOrigin");

// for authentication
app.UseMiddleware<HttpOnlyMiddleware>(secretsConfig.JwtConfig!.CookieName);
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.Run();
