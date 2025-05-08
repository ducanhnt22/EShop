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
        var dbPassword = builder.AddParameter("DbPassword", "AnhSE160517@");
        var dbServer = builder.AddPostgres("EShopDb", password: dbPassword, port: 5433);
        var userDb = dbServer.AddDatabase("eshop-user");
        var orderDb = dbServer.AddDatabase("eshop-order");
        var productDb = dbServer.AddDatabase("eshop-product");

        var cache = builder.AddRedis("CacheConnection");

        builder.AddProject<Projects.EShop_UserService_API>("userservice")
               .WithReference(userDb).WithReference(cache).WaitFor(userDb).WaitFor(cache);

        builder.AddProject<Projects.EShop_OrderService_API>("orderservice")
               .WithReference(orderDb).WithReference(cache).WaitFor(orderDb).WaitFor(cache);

        builder.AddProject<Projects.EShop_ProductService_API>("productservice")
               .WithReference(productDb).WithReference(cache).WaitFor(productDb).WaitFor(cache);

    }
}