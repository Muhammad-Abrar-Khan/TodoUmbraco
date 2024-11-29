using Todo.Common.Helpers;
using Serilog;
using System.Text.Json.Serialization;
using Todo.Common.Helpers;
using Umbraco.Cms.Web.Common.ApplicationBuilder;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configure Serilog from appsettings.json
// Clear other logging providers
builder.Logging.ClearProviders();

builder.Host.UseSerilog(Log.Logger);

// Increase max response size (for example, to 10 MB)
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
    serverOptions.Limits.MaxResponseBufferSize = 10 * 1024 * 1024; // 10 MB
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});


// 1. Add CORS service
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()    // Allows requests from all origins
                   .AllowAnyHeader()   // Allows any headers
                   .AllowAnyMethod();  // Allows any HTTP method
        });
});*/

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 100; // Adjust depth if needed
        options.JsonSerializerOptions.Converters.Add(new TypeJsonConverter()); // Add the custom converter
    });

//Initialize Logger
Common.Initialize();

WebApplication app = builder.Build();

//HTTP Request Logging
//app.UseSerilogRequestLogging();

// 2. Use CORS middleware before other middleware
/*app.UseCors("AllowSpecificOrigin");

// 3. Use the middleware to add custom security headers
app.Use((context, next) =>
{
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    return next();
});*/

await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();

