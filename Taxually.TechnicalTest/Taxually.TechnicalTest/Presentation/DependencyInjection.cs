using Taxually.TechnicalTest.Application.VatRegistration;

namespace Taxually.TechnicalTest.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<IVatRegistrationFactory, VatRegistrationFactory>();
            return services;
        }
    }
}
