using EShop.UserService.Application.Common.Authentication;
using EShop.UserService.Infrastructure.Authentication;
using EShop.UserService.Infrastructure.Repository.IRepositories;
using EShop.UserService.Infrastructure.Repository.Repositories;
using EShop.UserService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;
using EShop.UserService.Infrastructure.UnitOfWork.UnitOfWorkSetup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Infrastructure
{
    public static class DependencyInjection
    {
        public static object AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUnitOfWorks, UnitOfWorks>();
            return services;
        }
    }
}
