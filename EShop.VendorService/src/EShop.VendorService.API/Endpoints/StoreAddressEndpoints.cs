using EShop.VendorService.API.Data;
using EShop.VendorService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.VendorService.API.Endpoints
{
    public static class StoreAddressEndpoints
    {
        public record CreateStoreAddressRequest(string StreetAddress, string WardCode, int DistrictId, string WardName, string DistrictName, string ProvinceName);
        public static void MapStoreAddressEndpoints(this WebApplication app)
        {
            app.MapGet("/stores/{storeId:guid}/addresses", async (Guid storeId, AppDbContext context) =>
            {
                var addresses = await context.StoreAddresses
                    .Where(a => a.StoreId == storeId && !a.IsDeleted)
                    .ToListAsync();
                if (addresses == null || !addresses.Any())
                {
                    return Results.NotFound();
                }
                return Results.Ok(addresses);
            });

            app.MapPost("/stores/{storeId:guid}/addresses", async (Guid storeId, CreateStoreAddressRequest request, AppDbContext context) =>
            {
                var store = await context.Stores.FindAsync(storeId);
                if (store == null || store.IsDeleted)
                {
                    return Results.NotFound();
                }
                var address = new StoreAddress
                {
                    StoreId = storeId,
                    StreetAddress = request.StreetAddress,
                    WardCode = request.WardCode,
                    DistrictId = request.DistrictId,
                    WardName = request.WardName,
                    DistrictName = request.DistrictName,
                    ProvinceName = request.ProvinceName
                };
                context.StoreAddresses.Add(address);
                await context.SaveChangesAsync();
                return Results.Created($"/stores/{storeId}/addresses/{address.Id}", address);
            });
        }
    }
}
