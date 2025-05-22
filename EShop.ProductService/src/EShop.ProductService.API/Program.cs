using EShop.ProductService.API.Extensions;
using EShop.ProductService.Application;
using EShop.ProductService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.ConfigureDatabase(builder.Configuration);

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
//app.UseOutputCache();
app.MapDefaultEndpoints();

app.UseCustomMiddlewares();
app.MapDefaultEndpoints();
app.UseAuthorization();
app.MapControllers();
await app.SeedDatabaseAsync();

app.Run();
