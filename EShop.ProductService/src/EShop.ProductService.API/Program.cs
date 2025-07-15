using EShop.ProductService.API.Extensions;
using EShop.ProductService.Application;
using EShop.ProductService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Database configuration with connection pooling optimization
builder.Services.ConfigureDatabase(builder.Configuration);

// Enable output caching for better performance
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
        builder.Expire(TimeSpan.FromMinutes(5)));
});

// Add Redis distributed cache
builder.AddRedisDistributedCache(connectionName: "CacheConnection");

// Enable response compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
    options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
});

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddSwaggerWithJwt();
builder.Services.AddApplicationDI();
builder.Services.AddInfrastructureDI();

builder.Services.AddMediatRServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable response compression
app.UseResponseCompression();

// Enable output caching
app.UseOutputCache();

// Map default endpoints only once
app.MapDefaultEndpoints();

app.UseCustomMiddlewares();
app.UseAuthorization();
app.MapControllers();

// Only seed database in development to avoid performance impact
if (app.Environment.IsDevelopment())
{
    await app.SeedDatabaseAsync();
}

app.Run();
