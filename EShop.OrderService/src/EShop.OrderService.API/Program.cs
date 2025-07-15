var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Enable output caching for better performance
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
        builder.Expire(TimeSpan.FromMinutes(2))); // Shorter cache for orders
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

app.MapDefaultEndpoints();
app.UseAuthorization();
app.MapControllers();

app.Run();
