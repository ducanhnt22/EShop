using EShop.VendorService.API.Data;
using EShop.VendorService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.VendorService.API.Endpoints;

public static class StoreEndpoints
{
    public record CreateStoreRequest(string Name, string Description, string ImageUrl, List<CreateStoreAddressRequest> Addresses);
    public record CreateStoreAddressRequest(string StreetAddress, string WardCode, int DistrictId, string WardName, string DistrictName, string ProvinceName);
    public record StoreResponse(Guid Id, string Name, string Description, string ImageUrl, List<StoreAddressResponse> Addresses);
    public record StoreAddressResponse(string StreetAddress, string WardCode, int DistrictId, string WardName, string DistrictName, string ProvinceName);
    public record GetStoreRequest(Guid? Id);
    public static void MapStoreEndpoints(this WebApplication app)
    {
        app.MapGet("/stores", async (AppDbContext context) =>
        {
            var stores = await context.Stores
                .Include(s => s.Addresses)
                .Where(s => s.IsDeleted == false)
                .ToListAsync();

            var response = stores.Select(store => new StoreResponse(
                store.Id,
                store.Name,
                store.Description,
                store.ImageUrl,
                store.Addresses.Select(a => new StoreAddressResponse(
                    a.StreetAddress,
                    a.WardCode,
                    a.DistrictId,
                    a.WardName,
                    a.DistrictName,
                    a.ProvinceName
                )).ToList()
            )).ToList();

            return response != null && response.Any() ? Results.Ok(response) : Results.NotFound();
        });

        app.MapGet("/stores/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var store = await context.Stores
                .Include(s => s.Addresses)
                .FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted == false);

            var response = new StoreResponse(
                store.Id,
                store.Name,
                store.Description,
                store.ImageUrl,
                store.Addresses.Select(a => new StoreAddressResponse(
                    a.StreetAddress,
                    a.WardCode,
                    a.DistrictId,
                    a.WardName,
                    a.DistrictName,
                    a.ProvinceName
                )).ToList()
            );
            return response is null ? Results.NotFound() : Results.Ok(response);
        });

        app.MapPost("/stores", async (CreateStoreRequest request, AppDbContext context) =>
        {
            var store = new Store
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Addresses = request.Addresses.Select(a => new StoreAddress
                {
                    StreetAddress = a.StreetAddress,
                    WardCode = a.WardCode,
                    DistrictId = a.DistrictId,
                    WardName = a.WardName,
                    DistrictName = a.DistrictName,
                    ProvinceName = a.ProvinceName,
                }).ToList()
            };

            context.Stores.Add(store);
            await context.SaveChangesAsync();

            var response = new StoreResponse(
                store.Id,
                store.Name,
                store.Description,
                store.ImageUrl,
                store.Addresses.Select(a => new StoreAddressResponse(
                    a.StreetAddress,
                    a.WardCode,
                    a.DistrictId,
                    a.WardName,
                    a.DistrictName,
                    a.ProvinceName
                )).ToList()
            );

            return Results.Created($"/stores/{store.Id}", response);
        });

        app.MapPatch("/stores/{id:guid}", async(Guid Id, AppDbContext context) =>
        {
            var store = await context.Stores
                .Include(s => s.Addresses)
                .FirstOrDefaultAsync(s => s.Id == Id && s.IsDeleted == false);
            if (store is null)
            {
                return Results.NotFound();
            }
            store.IsDeleted = true;
            context.Stores.Update(store);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/stores/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var store = await context.Stores
                .Include(s => s.Addresses)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (store is null)
            {
                return Results.NotFound();
            }
            store.IsDeleted = true; 
            context.Stores.Update(store);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
