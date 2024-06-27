namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public class VatRegistrationFactory : IVatRegistrationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public VatRegistrationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IVatRegistration GetVatRegister(string country)
        {
            switch (country)
            {
                case "GB":
                    return _serviceProvider.GetRequiredService<VatRegistrationGB>();
                case "FR":
                    return _serviceProvider.GetRequiredService<VatRegistrationFR>();
                case "DE":
                    return _serviceProvider.GetRequiredService<VatRegistrationDE>();
                default:
                    throw new Exception("Country not supported");
            }
        }
    }
}
