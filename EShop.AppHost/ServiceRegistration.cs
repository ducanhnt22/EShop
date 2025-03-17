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
        builder.AddProject<Projects.EShop_UserService_API>("userservice");
        builder.AddProject<Projects.EShop_OrderService_API>("orderservice");
        builder.AddProject<Projects.EShop_ProductService_API>("productservice");
    }
}