using Is.Api.Configurations;
using Is.Core.Abstraction;
using Is.Core.Api;
using Is.Core.ApiConfig;
using Is.Core.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
app.UseAtsDatabase();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();
