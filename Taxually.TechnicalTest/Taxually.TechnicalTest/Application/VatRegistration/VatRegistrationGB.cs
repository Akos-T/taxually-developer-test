using Taxually.TechnicalTest.Infrastructure;

namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public class VatRegistrationGB : IVatRegistration
    {
        private readonly TaxuallyHttpClient _httpClient;

        public VatRegistrationGB(TaxuallyHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Register(VatRegistrationRequest request)
        {
            // UK has an API to register for a VAT number
            await _httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }
    }
}
