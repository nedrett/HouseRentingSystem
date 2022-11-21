namespace Microsoft.Extensions.DependencyInjection
{
    using HouseRentingSystem.Core.Contracts;
    using HouseRentingSystem.Core.Exceptions;
    using HouseRentingSystem.Core.Services;
    using HouseRentingSystem.Infrastructure.Data.Common;

    public static class HouseRentingSystemServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IGuard, Guard>();

            return services;
        }
    }
}
