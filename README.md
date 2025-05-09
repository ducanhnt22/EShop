# 🛒 EShop Microservices

**EShop** là một hệ thống thương mại điện tử được xây dựng theo kiến trúc **Microservices**. Dự án sử dụng nền tảng **.NET Aspire** để điều phối các dịch vụ trong môi trường phát triển, với các thành phần như PostgreSQL, Redis, RabbitMQ, v.v.

---

## ⚙️ Kiến trúc tổng quan

Dự án gồm các dịch vụ chính:

- `UserService`: Quản lý người dùng
- `OrderService`: Quản lý đơn hàng
- `ProductService`: Quản lý sản phẩm
- `RabbitMQSender`: Gửi message đến RabbitMQ
- `RabbitMQReceiver`: Nhận và xử lý message từ RabbitMQ
- `Redis`: Hệ thống cache
- `PostgreSQL`: Cơ sở dữ liệu chính
- `Aspire.AppHost`: Điều phối toàn bộ service

---

## 🧱 Công nghệ sử dụng

| Thành phần        | Mục đích                             |
|------------------|--------------------------------------|
| .NET 8 + Aspire  | Phát triển và điều phối microservices |
| ASP.NET Core     | Web API                              |
| PostgreSQL       | CSDL chính                           |
| Redis            | Distributed cache                    |
| RabbitMQ         | Message Broker                       |
| Aspire Dashboard | Giám sát và quản lý toàn bộ hệ thống |
| Docker (nội bộ Aspire) | Chạy các service phụ trợ       |

---

## 🚀 Hướng dẫn chạy hệ thống

### 1. Cài đặt yêu cầu

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (bắt buộc cho Aspire)
- Aspire workload:

```bash
dotnet workload install aspire
```
### 2. Chạy toàn bộ hệ thống
```bash
cd EShop.AppHost
dotnet run
```

### 🧪 Tính năng hiện tại

✅ Tách database riêng cho từng service

✅ Redis làm cache (có thể dùng RedisInsight để quan sát)

✅ RabbitMQ xử lý message bất đồng bộ

✅ Các service cấu hình bằng Aspire (cấu trúc rõ ràng, scale tốt)

## 🛠 Kế hoạch mở rộng

| Tính năng                            | Trạng thái           |
|-------------------------------------|----------------------|
| Event Sourcing (Kafka)              | 🔜 Đang lên kế hoạch |
| Logging tập trung (ELK/Prometheus)  | 🔜 Đang lên kế hoạch |
| Authentication + JWT                | 🔜 Đang lên kế hoạch |
| Load testing (k6)                   | 🔜 Đang lên kế hoạch |
| UI Web/App cho khách hàng           | 🔜 Đang lên kế hoạch |

## 📝 Conclusion

This **EShop** project is a part of my self-learning journey in software development and system design. Through this project, I have gained hands-on experience with building **Microservices** architecture and working with technologies such as **.NET Aspire**, **RabbitMQ**, **Redis**, **PostgreSQL**, and various techniques for managing distributed services.

I hope this project will continue to evolve and expand with more features in the future. It serves as an opportunity for me to practice and enhance my skills.

---

Thank you for taking the time to explore this project!
