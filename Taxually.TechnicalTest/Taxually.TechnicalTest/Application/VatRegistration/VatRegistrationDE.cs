using System.Xml.Serialization;
using Taxually.TechnicalTest.Controllers;

namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public class VatRegistrationDE : IVatRegistration
    {
        public async Task Register(VatRegistrationRequest request)
        {
            // Germany requires an XML document to be uploaded to register for a VAT number
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
                serializer.Serialize(stringwriter, this);
                var xml = stringwriter.ToString();
                var xmlQueueClient = new TaxuallyQueueClient();
                // Queue xml doc to be processed
                await xmlQueueClient.EnqueueAsync("vat-registration-xml", xml);
            }
        }
    }
}
