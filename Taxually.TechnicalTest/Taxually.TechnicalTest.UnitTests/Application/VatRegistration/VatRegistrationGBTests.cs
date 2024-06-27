using Moq;
using Taxually.TechnicalTest.Application.VatRegistration;
using Taxually.TechnicalTest.Infrastructure;

namespace Taxually.TechnicalTest.UnitTests.Application.VatRegistration
{
    [TestClass]
    public class VatRegistrationGBTests
    {
        [TestMethod]
        public async Task TestRegister_ApiCalled_Success()
        {
            //Arrange
            var registrationRequest = new VatRegistrationRequest("CompanyName", "CompanyId", "GB");
            var mockHttpClient = new Mock<TaxuallyHttpClient>();
            var registration = new VatRegistrationGB(mockHttpClient.Object);

            //Act
            await registration.Register(registrationRequest);

            //Assert
            mockHttpClient.Verify(client => client.PostAsync(It.IsAny<string>(), registrationRequest), Times.Once);
            mockHttpClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        [DataRow("", "companyId", "GB")]
        [DataRow("company name", "", "GB")]
        [DataRow("company name", "companyId", "")]
        public async Task TestRegister_ValidationFails_ThrowsException(string companyName, string companyId, string country)
        {
            // Arrange
            var registrationRequest = new VatRegistrationRequest(companyName, companyId, country);
            var mockHttpClient = new Mock<TaxuallyHttpClient>();
            var registration = new VatRegistrationGB(mockHttpClient.Object);

            //Act
            try
            {
                await registration.Register(registrationRequest);
                Assert.Fail($"Expected {nameof(ArgumentException)}, but got no exception at all");
            }
            catch (ArgumentException)
            {
                // This is what we expect so no fail here
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected: {nameof(ArgumentException)}, but got: {ex.GetType()}");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public async Task TestRegister_ApiCallTimesOut_ThrowsException()
        {
            // Arrange
            var registrationRequest = new VatRegistrationRequest("Company Name", "CompanyId", "GB");
            var mockHttpClient = new Mock<TaxuallyHttpClient>();
            mockHttpClient.Setup(client => client.PostAsync(It.IsAny<string>(), registrationRequest)).ThrowsAsync(new TimeoutException());
            var registration = new VatRegistrationGB(mockHttpClient.Object);

            // Act
            await registration.Register(registrationRequest);
        }
    }
}
