using EShop.UserService.API.Extensions;
using EShop.UserService.Application;
using EShop.UserService.Infrastructure;
using EShop.UserService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDatabase(builder.Configuration);
//builder.AddNpgsqlDbContext<UserDbContext>("eshop-user");

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

app.UseCustomMiddlewares();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.SeedDatabaseAsync();

app.Run();
