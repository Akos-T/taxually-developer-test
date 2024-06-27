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
            // France requires an excel spreadsheet to be uploaded to register for a VAT number
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");

            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            // Queue file to be processed
            await _excelQueueClient.EnqueueAsync(EXCEL_QUEUE_NAME, csv);
        }
    }
}
