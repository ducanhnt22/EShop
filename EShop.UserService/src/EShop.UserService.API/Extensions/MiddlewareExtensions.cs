using EShop.UserService.Application.Common.Exceptions;

namespace EShop.UserService.API.Extensions;
public static class MiddlewareExtensions
{
    public static void UseCustomMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}