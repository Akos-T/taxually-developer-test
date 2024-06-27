using Taxually.TechnicalTest.Controllers;

namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public interface IVatRegistration
    {
        public Task Register(VatRegistrationRequest request);
    }
}
