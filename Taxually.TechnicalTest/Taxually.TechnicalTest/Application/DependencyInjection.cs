using Taxually.TechnicalTest.Application.VatRegistration;

namespace Taxually.TechnicalTest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<VatRegistrationGB>();
            services.AddScoped<VatRegistrationFR>();
            services.AddScoped<VatRegistrationDE>();
            services.AddScoped<IVatRegistrationFactory, VatRegistrationFactory>();
            return services;
        }
    }
}
