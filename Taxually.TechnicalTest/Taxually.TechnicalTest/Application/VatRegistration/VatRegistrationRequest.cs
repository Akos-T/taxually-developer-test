// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public class VatRegistrationRequest
    {
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string Country { get; set; }

        // Default constructor is required for model binding and serialization
        public VatRegistrationRequest()
        {
            CompanyName = string.Empty;
            CompanyId = string.Empty;
            Country = string.Empty;
        }

        public VatRegistrationRequest(string companyName = "", string companyId = "", string country = "")
        {
            CompanyName = companyName;
            CompanyId = companyId;
            Country = country;
        }
    }
}
