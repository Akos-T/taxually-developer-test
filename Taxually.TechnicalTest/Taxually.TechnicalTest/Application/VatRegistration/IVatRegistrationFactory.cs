namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public interface IVatRegistrationFactory
    {
        public IVatRegistration GetVatRegister(string country);
    }
}
