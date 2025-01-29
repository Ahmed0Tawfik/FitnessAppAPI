using Fitness.Application;
using Fitness.Application.Auth;
using FluentValidation;

namespace Fitness.API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserLoginCommand.Validator>();
            return services;
        }

        public static IServiceCollection AddAutoRegisterHandlers(this IServiceCollection services)
        {
            var handlerInterface = typeof(IRequestHandler<,>);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes()
                    .Where(t => t.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)))
                {
                    foreach (var interfaceType in type.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface))
                    {
                        services.AddTransient(interfaceType, type);
                    }
                }
            }
            return services;
        }
    }
}
