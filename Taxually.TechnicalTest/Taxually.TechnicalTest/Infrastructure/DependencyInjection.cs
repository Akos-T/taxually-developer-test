namespace Taxually.TechnicalTest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<TaxuallyHttpClient>();
            services.AddScoped<TaxuallyQueueClient>();
            return services;
        }
    }
}
