using APICatalogo.Context;
// using APICatalogo.Extensions;
using APICatalogo.Filters;
using APICatalogo.Logging;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter)); // Global Filter for untreated Exceptions
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Ignores object serialization when a ciclycal reference is detected
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Reading from configuration files
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// var valor1 = builder.Configuration["chave1"];
// var valor2 = builder.Configuration["secao1:chave2"];


builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddScoped<ApiLoggingFilter>();

// AddScoped creates one instance per request. So we'll use the same instance of a repo and their parents inside a request!
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>(); // Links every usage of ICategoriaRepository to the concrete class CategoriaRepository
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // AddScoped doesn't allow for generic arguments, so we'll need to add them like this
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Defining a swagger middleware
    app.UseSwaggerUI();
    // app.ConfigureExceptionHandler(); // Global error handling middleware, not needed because of the filter
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Includes routing middleware, mapping every attribute according to the conventions.
//      For explicitly enabling routing, use:
//      - app.UseRouting(): Adds route correspondence to the pipeline;
//      - app.UseEndpoints(): Adds endpoint execution to the pipeline;
app.MapControllers();

// Defines a terminal middleware to the request pipeline, generates a response
app.Run();
