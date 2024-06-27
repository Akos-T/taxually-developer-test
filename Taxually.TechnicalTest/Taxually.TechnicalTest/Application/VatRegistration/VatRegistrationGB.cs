namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public class VatRegistrationGB : IVatRegistration
    {
        public async Task Register(VatRegistrationRequest request)
        {
            // UK has an API to register for a VAT number
            var httpClient = new TaxuallyHttpClient();
            await httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }
    }
}
