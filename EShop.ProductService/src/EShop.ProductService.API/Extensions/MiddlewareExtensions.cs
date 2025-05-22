using EShop.ProductService.Application.Common.Behaviours;

namespace EShop.ProductService.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
