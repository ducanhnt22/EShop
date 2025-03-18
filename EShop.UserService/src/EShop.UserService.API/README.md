# Database Migrations

## 1. Tạo Migration
Chạy lệnh sau để tạo migration mới:

dotnet ef migrations add InitialCreate -p src/EShop.UserService.Infrastructure -s src/EShop.UserService.API -o Persistence/Migrations

## 2. Chạy lệnh để áp dụng migration lên database:
dotnet ef database update -p src/EShop.UserService.Infrastructure -s src/EShop.UserService.API
