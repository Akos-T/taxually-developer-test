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
            var validationError = await ValidateVatRegistrationRequest(request);
            if (validationError != null) {
                throw validationError;
            }

            // UK has an API to register for a VAT number
            await _httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }

        private Task<Exception?> ValidateVatRegistrationRequest(VatRegistrationRequest request) 
        {
            // This first could be encapsulated and reused in the other registration classes!
            if (request == null)
            {
                return Task.FromException<Exception?>(new ArgumentException($"{nameof(request)} is required"));
            }
            if (string.IsNullOrWhiteSpace(request.CompanyName))
            {
                return Task.FromException<Exception?>(new ArgumentException($"{nameof(request.CompanyName)} is required"));
            }
            if (string.IsNullOrWhiteSpace(request.CompanyId))
            {
                return Task.FromException<Exception?>(new ArgumentException($"{nameof(request.CompanyId)} is requiered"));
            }
            if (string.IsNullOrWhiteSpace(request.Country))
            {
                return Task.FromException<Exception?>(new ArgumentException($"{nameof(request.Country)} is required"));
            }

            // Add here any business logic validation for input parameters

            return Task.FromResult<Exception?>(null);
        }
    }
}
