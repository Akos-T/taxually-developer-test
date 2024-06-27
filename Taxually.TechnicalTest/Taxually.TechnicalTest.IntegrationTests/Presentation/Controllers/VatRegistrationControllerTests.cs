using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Taxually.TechnicalTest.Application.VatRegistration;

namespace Taxually.TechnicalTest.IntegrationTests.Presentation.Controllers
{
    [TestClass]
    public class VatRegistrationControllerTests
    {
        [TestMethod]
        [DataRow("GB")]
        [DataRow("FR")]
        [DataRow("DE")]
        public async Task TestVatRegistration_ValidCountries_Success(string country)
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // We could use a mock service here, but we don't have to
                    //builder.ConfigureServices(services =>
                    //{
                    //    services.AddScoped<IService, MockService>();
                    //});
                });

            var client = application.CreateClient();

            var request = new VatRegistrationRequest("Company Name", "CompanyId", country);
            var response = await client.PostAsJsonAsync("/api/VatRegistration", request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task TestVatRegistration_NullRequest_ThrowsException()
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            var response = await client.PostAsJsonAsync<VatRegistrationRequest>("/api/VatRegistration", null);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // We could add other test cases as well, e.g.:
        // - Country is "" => InternalServerError
        // - CompanyName or CompandId is empty => InternalServerError
        // - Etc.
    }
}