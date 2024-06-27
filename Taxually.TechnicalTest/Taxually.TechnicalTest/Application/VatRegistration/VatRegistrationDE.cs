using System.Xml.Serialization;
using Taxually.TechnicalTest.Infrastructure;

namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public class VatRegistrationDE : IVatRegistration
    {
        private const string XML_QUEUE_NAME = "vat-registration-xml"; // This could be moved to appsettings.json and get the value from there using the Options pattern
        
        private readonly TaxuallyQueueClient _xmlQueueClient;

        public VatRegistrationDE(TaxuallyQueueClient xmlQueueClient)
        {
            _xmlQueueClient = xmlQueueClient;
        }

        public async Task Register(VatRegistrationRequest request)
        {
            var validationError = await ValidateVatRegistrationRequest(request);
            if (validationError != null)
            {
                throw validationError;
            }

            // Germany requires an XML document to be uploaded to register for a VAT number
            using var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringWriter, request);

            var xml = stringWriter.ToString();

            // Queue xml doc to be processed
            await _xmlQueueClient.EnqueueAsync(XML_QUEUE_NAME, xml);
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
