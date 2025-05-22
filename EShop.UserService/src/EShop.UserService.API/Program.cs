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
app.UseOutputCache();
app.MapDefaultEndpoints();

app.UseCustomMiddlewares();
app.UseCors("AllowGateway");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.SeedDatabaseAsync();

app.Run();
