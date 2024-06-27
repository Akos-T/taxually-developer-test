using System.Text;
using Taxually.TechnicalTest.Infrastructure;

namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public class VatRegistrationFR : IVatRegistration
    {
        private const string EXCEL_QUEUE_NAME = "vat-registration-csv";

        private readonly TaxuallyQueueClient _excelQueueClient;

        public VatRegistrationFR(TaxuallyQueueClient excelQueueClient)
        {
            _excelQueueClient = excelQueueClient;
        }

        public async Task Register(VatRegistrationRequest request)
        {
            var validationError = await ValidateVatRegistrationRequest(request);
            if (validationError != null)
            {
                throw validationError;
            }

            // France requires an excel spreadsheet to be uploaded to register for a VAT number
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");

            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            // Queue file to be processed
            await _excelQueueClient.EnqueueAsync(EXCEL_QUEUE_NAME, csv);
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
