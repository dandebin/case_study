using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Reconcile.BusinessLogic;
using Reconcile.Repository;
using ReconcileService;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var env = builder.Environment;

services.AddCors();
services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
}).AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
}).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
         new BadRequestObjectResult(context.ModelState)
         {
             ContentTypes =
                {
                    // using static System.Net.Mime.MediaTypeNames;
                    Application.Json,
                    Application.Xml
                }
         };
}).AddXmlSerializerFormatters();
services.AddProblemDetails();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
// configure strongly typed settings object
services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

// configure DI for application services
services.AddSingleton<DataContext>();
services.AddTransient<IInsuranceRepository, InsuranceRepository>();
services.AddTransient<ICounterPartyRepository, CounterPartyRepository>();
services.AddTransient<IArapJdeRepository, ArapJdeRepository>();
services.AddTransient<IReconcileRepository, ReconcileRepository>();
services.AddScoped<IReconcileReport, ReconcileReport>();
services.AddScoped<IMemoryCache, MemoryCache>();

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");
logger.LogInformation("Hello World! Logging is {Description}.", "fun");


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStatusCodePages();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<AutoLogMiddleWare>();
app.Run();

