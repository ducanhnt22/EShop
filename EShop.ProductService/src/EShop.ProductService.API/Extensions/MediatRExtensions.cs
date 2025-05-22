namespace EShop.ProductService.API.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
            return services;
        }
    }
}
