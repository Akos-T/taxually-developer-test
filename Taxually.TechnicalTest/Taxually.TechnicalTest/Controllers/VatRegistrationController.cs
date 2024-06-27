using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Application.VatRegistration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            switch (request.Country)
            {
                case "GB":
                    var registrationGB = new VatRegistrationGB();
                    await registrationGB.Register(request);
                    break;
                case "FR":
                    var registrationFR = new VatRegistrationFR();
                    await registrationFR.Register(request);
                    break;
                case "DE":
                    var registrationDE = new VatRegistrationFR();
                    await registrationDE.Register(request);
                    break;
                default:
                    throw new Exception("Country not supported");

            }
            return Ok();
        }
    }
}
