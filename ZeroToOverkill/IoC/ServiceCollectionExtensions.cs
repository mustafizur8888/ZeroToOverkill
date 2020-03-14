using Business.Impl.Services;
using Business.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IGroupService, InMemoryGroupService>();
            return services;
        }
    }
}
