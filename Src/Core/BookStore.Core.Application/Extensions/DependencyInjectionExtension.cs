using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Core.Application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
