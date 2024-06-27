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
            // Germany requires an XML document to be uploaded to register for a VAT number
            using var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringWriter, request);

            var xml = stringWriter.ToString();

            // Queue xml doc to be processed
            await _xmlQueueClient.EnqueueAsync(XML_QUEUE_NAME, xml);
        }
    }
}
