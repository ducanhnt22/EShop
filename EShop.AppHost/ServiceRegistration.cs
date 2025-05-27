namespace EShop.AppHost;

public static class ServiceRegistration
{
    public static void RegisterServices(IDistributedApplicationBuilder builder)
    {
        //PostgreSQL
        var dbPassword = builder.AddParameter("DbPassword", "AnhSE160517@");
        var dbServer = builder.AddPostgres("EShopDb", password: dbPassword, port: 5433);
        var userDb = dbServer.AddDatabase("eshop-user");
        var orderDb = dbServer.AddDatabase("eshop-order");
        var productDb = dbServer.AddDatabase("eshop-product");
        var inventoryDb = dbServer.AddDatabase("eshop-inventory");
        var vendorDb = dbServer.AddDatabase("eshop-vendor");

        //Redis
        var redisPassword = builder.AddParameter("RedisPassword", "12345");
        var cache = builder.AddRedis("CacheConnection", port: 6379, redisPassword);

        //RabbitMQ
        var rabbitUser = builder.AddParameter("RabbitMQUser", "guest");
        var rabbitPassword = builder.AddParameter("RabbitMQPassword", "12345");
        var rabbit = builder.AddRabbitMQ("messaging", rabbitUser, rabbitPassword, port: 5672).WithManagementPlugin();

        builder.AddProject<Projects.Eshop_RabbitMQSenders>("rabbitmqsender")
               .WithReference(rabbit).WaitFor(rabbit);

        builder.AddProject<Projects.EShop_RabbitMQReceivers>("rabbitmqreceiver")
               .WithReference(rabbit).WaitFor(rabbit);

        //Kafka
        var kafka = builder.AddKafka("kafka", port: 9092)
                        .WithKafkaUI(kafkaUI => kafkaUI.WithHostPort(9100));

        //Projects
        var userApi = builder.AddProject<Projects.EShop_UserService_API>("userservice")
                       .WithReference(userDb)
                       .WithReference(cache)
                       .WithReference(rabbit)
                       .WithReference(kafka)
                       .WaitFor(userDb)
                       .WaitFor(cache);
        //Custom endpoint to access the Swagger UI
        userApi.WithUrl($"{userApi.GetEndpoint("https")}/swagger/index.html", "User Portal");

        var orderApi = builder.AddProject<Projects.EShop_OrderService_API>("orderservice")
                              .WithReference(orderDb)
                              .WithReference(cache)
                              .WithReference(rabbit)
                              .WithReference(kafka)
                              .WaitFor(orderDb)
                              .WaitFor(cache);
        orderApi.WithUrl($"{orderApi.GetEndpoint("https")}/swagger/index.html", "Order Portal");

        var productApi = builder.AddProject<Projects.EShop_ProductService_API>("productservice")
                                .WithReference(productDb)
                                .WithReference(cache)
                                .WithReference(rabbit)
                                .WithReference(kafka)
                                .WaitFor(productDb)
                                .WaitFor(cache);
        productApi.WithUrl($"{productApi.GetEndpoint("https")}/swagger/index.html", "Product Portal");

        var vendorApi = builder.AddProject<Projects.EShop_VendorService_API>("vendorservice")
                                .WithReference(vendorDb)
                                .WithReference(cache)
                                .WithReference(rabbit)
                                .WithReference(kafka)
                                .WaitFor(productDb)
                                .WaitFor(cache);
        vendorApi.WithUrl($"{vendorApi.GetEndpoint("https")}/swagger/index.html", "Vendor Portal");

        var inventoryApi = builder.AddProject<Projects.EShop_InventoryService_API>("inventoryservice")
                                  .WithReference(inventoryDb)
                                  .WithReference(cache)
                                  .WithReference(rabbit)
                                  .WithReference(kafka)
                                  .WaitFor(inventoryDb)
                                  .WaitFor(cache);
        inventoryApi.WithUrl($"{inventoryApi.GetEndpoint("https")}/swagger/index.html", "Inventory Portal");
    }
}
