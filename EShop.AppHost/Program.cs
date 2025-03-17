using EShop.AppHost;

var builder = DistributedApplication.CreateBuilder(args);
ServiceRegistration.RegisterServices(builder);

builder.Build().Run();
