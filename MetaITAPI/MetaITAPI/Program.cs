using System.Reflection;
using System.Text.Json.Serialization;
using MetaITAPI.Data;
using MetaITAPI.Interfaces;
using MetaITAPI.Repository;
using MetaITAPI.Services;
using MetaITAPI.Utils.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

// For Development and Production will be different connectionString
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var connectionString = builder.Configuration.GetConnectionString(env);

// Add services to the container.

// Do we need restrict origins?
//builder.Services.AddCors();

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

builder.Services.AddLogging(loggingBuilder =>
{
    if (env == Environments.Production)
    {
        // loggingBuilder.Services.AddApplicationInsightsTelemetry(options =>
        // {
        //     options.ConnectionString = builder.Configuration.GetValue<string>("ApplicationInsights:ConnectionString");
        // });
        // loggingBuilder.AddApplicationInsights();

        // but for our purposes just use console
        loggingBuilder.AddConsole();
    }
    else
    {
        loggingBuilder.AddConsole();
    }
});

// will be used for JWT token later
//builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

if (env == Environments.Development)
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

//-----------------------------------------------------------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

// Exceptions Handler
app.UseExceptionHandler(ExceptionHandlerHelper.JsonHandler());

app.UseHttpsRedirection();
app.UseRouting();


// Authentication & Authorization
//app.UseAuthentication(); //needs to be implemented in case of IdentityProvider
app.UseAuthorization();

// Middlewares
//app.UseMiddleware<AccessForbiddenMiddleware>(); //needs to be implemented in case of IdentityProvider

app.MapControllers();

app.Run();
