using Aspire.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.AppHost;

public static class ServiceRegistration
{
    public static void RegisterServices(IDistributedApplicationBuilder builder)
    {
        var userDb = builder.AddPostgres("UserDbConnection").AddDatabase("eshop-user");
        var orderDb = builder.AddPostgres("OrderDbConnection").AddDatabase("eshop-order");
        var productDb = builder.AddPostgres("ProductDbConnection").AddDatabase("eshop-product");

        var cache = builder.AddRedis("CacheConnection");

        builder.AddProject<Projects.EShop_UserService_API>("userservice")
               .WithReference(userDb).WithReference(cache).WaitFor(userDb).WaitFor(cache);

        builder.AddProject<Projects.EShop_OrderService_API>("orderservice")
               .WithReference(orderDb);

        builder.AddProject<Projects.EShop_ProductService_API>("productservice")
               .WithReference(productDb);

    }
}