using Business.Impl.Services;
using Business.Services;
using ZeroToOverkill.Filter;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRequiredMvcComponents(this IServiceCollection services)
        {
            services.AddTransient<ApiExceptionFilter>();
            services.AddMvcCore(option =>
            {
                option.EnableEndpointRouting = false;
                option.Filters.AddService<ApiExceptionFilter>();
            });
            return services;
        }
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IGroupService, InMemoryGroupService>();
            return services;
        }
    }
}
