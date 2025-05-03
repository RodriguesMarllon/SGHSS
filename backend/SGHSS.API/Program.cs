using Api.Filters;
using Api.Middleware;
using Application.Common;
using Domain.Configuration;
using Domain.Interfaces.Requests;
using Infrastructure.Common;
using Infrastructure.Configuration;
using Infrastructure.Services.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;
using Polly;
using Scalar.AspNetCore;
using Domain.Service.Abstract;
using Serilog;
using System.Security.Claims;
using Serilog.Context;

SetUpLogging();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Limits.MaxRequestBodySize = 2147483647;
    });
}

builder.Services.AddDbContext<SGHSSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SGHSSConnection"), op => op.CommandTimeout(600)));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogActionFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
})
.AddControllersAsServices();

//builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ApiSettings>(builder.Configuration);

builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>()
    .AddTransientHttpErrorPolicy(policy =>
        policy.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

builder.Services.AddApplication();
builder.Services.AddServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Data API", Version = "v1" });
    c.OperationFilter<RemoveUnusedParametersFilter>();
    c.SchemaFilter<EnumSchemaFilter>();
});

var app = builder.Build();

app.UseAuthentication();

app.Use((httpContext, next) =>
{
    const string PROTOCOL = "Protocol";
    var httpProtocol = httpContext.Request.Protocol;

    const string HTTP_SCHEME = "Scheme";
    var httpScheme = httpContext.Request.Scheme;

    var userIdClaim = httpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);

    using (LogContext.PushProperty(PROTOCOL, httpProtocol))
    using (LogContext.PushProperty("UserId", userIdClaim?.Value ?? "unkow"));
    using (LogContext.PushProperty(HTTP_SCHEME, httpScheme))
    {
        return next();
    }
});

ServiceProviderHelper.ServiceProvider = app.Services;

// Use CORS
app.UseCors("AllowReactApp");

app.MapOpenApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();

    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.ConfigObject.AdditionalItems["theme"] = "scalars";
        config.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
        {
            ["activated"] = false
        };
    });
}

app.UseExceptionMiddleware();

// Desabilita redirecionamento HTTPS em desenvolvimento
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SetUpLogging()
{
    var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
    Directory.CreateDirectory(basePath); // Garante que o diretório existe
    var logPath = Path.Combine(basePath, "SGHSS-{Date}-logging-json.log");

    Serilog.Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting", Serilog.Events.LogEventLevel.Information)
        .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
        .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
        .WriteTo.Seq(
            "http://localhost:5341",
            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
        )
        .Enrich.WithProperty("App", "SGHSS-001")
        .Enrich.FromLogContext()
        .CreateBootstrapLogger();
}

public class RemoveUnusedParametersFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters != null)
        {
            operation.Parameters = operation.Parameters
                .Where(p => p.Name != "ContentType" && p.Name != "ContentDisposition" && p.Name != "Headers")
                .ToList();
        }
    }
}

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var enumNames = Enum.GetNames(context.Type);
            var enumValues = Enum.GetValues(context.Type);

            schema.Enum.Clear();
            foreach (var enumValue in enumValues)
            {
                // Add enum description if it exists
                var enumName = enumNames[Array.IndexOf(enumValues, enumValue)];
                var description = enumValue.ToString();

                // Add the value along with description
                schema.Enum.Add(new OpenApiString(description));
            }
        }
    }
}
