using EShop.UserService.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies([AssemblyReference.Executing]);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>),
                        ServiceLifetime.Scoped);
        });
        return services;
    }
}
