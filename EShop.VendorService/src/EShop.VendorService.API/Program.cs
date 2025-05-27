using EShop.VendorService.API.Endpoints;
using EShop.VendorService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services.ConfigureDatabase(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapStoreEndpoints();
app.MapStoreAddressEndpoints();

app.UseHttpsRedirection();
await app.SeedDatabaseAsync();

app.Run();