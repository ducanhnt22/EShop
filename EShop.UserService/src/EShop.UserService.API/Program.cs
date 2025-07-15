using EShop.UserService.API.Extensions;
using EShop.UserService.Application;
using EShop.UserService.Infrastructure;
using EShop.UserService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddOutputCache();
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
builder.Services.ConfigureIdentity();

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

app.UseOutputCache();
app.MapDefaultEndpoints();

app.UseCustomMiddlewares();
app.UseCors("AllowGateway");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Only seed database in development to avoid performance impact
if (app.Environment.IsDevelopment())
{
    await app.SeedDatabaseAsync();
}

app.Run();
