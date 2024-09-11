using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UptimeTeatmik.Application.Common.Behaviors;

namespace UptimeTeatmik.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(DependencyInjection)
                    .Assembly); // Automatically inject all commands and queries
            })
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}