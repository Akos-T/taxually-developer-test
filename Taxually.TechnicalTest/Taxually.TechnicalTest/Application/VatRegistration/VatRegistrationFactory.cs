namespace Taxually.TechnicalTest.Application.VatRegistration
{
    public class VatRegistrationFactory : IVatRegistrationFactory
    {
        public IVatRegistration GetVatRegister(string country)
        {
            switch (country)
            {
                case "GB":
                    return new VatRegistrationGB();
                case "FR":
                    return new VatRegistrationFR();
                case "DE":
                    return new VatRegistrationDE();
                default:
                    throw new Exception("Country not supported");
            }
        }
    }
}
