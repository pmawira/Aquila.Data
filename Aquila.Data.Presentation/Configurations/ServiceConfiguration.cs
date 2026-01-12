using Aquila.Data.Core.Engine;
using Aquila.Data.Presentation.Repositories;

namespace Aquila.Data.Presentation.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<DatabaseEngine>();
            return services;
        }
    }
}
