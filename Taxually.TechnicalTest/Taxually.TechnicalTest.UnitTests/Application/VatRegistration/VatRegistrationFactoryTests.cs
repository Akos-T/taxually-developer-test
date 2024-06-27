using Microsoft.Extensions.DependencyInjection;
using Moq;
using Taxually.TechnicalTest.Application.VatRegistration;
using Taxually.TechnicalTest.Infrastructure;

namespace Taxually.TechnicalTest.UnitTests.Application.VatRegistration
{
    [TestClass]
    public class VatRegistrationFactoryTests
    {
        [TestMethod]
        [DataRow("GB", typeof(VatRegistrationGB))]
        [DataRow("FR", typeof(VatRegistrationFR))]
        [DataRow("DE", typeof(VatRegistrationDE))]
        public void TestGetVatRegistration_NotEmptyCountry_ReturnsCorrectIVatRegistrationImplementation(string country, Type expectedIVatRegistrationImplementation)
        {
            // Arrange
            var mockTaxuallyHttpClient = new Mock<TaxuallyHttpClient>();
            var mockTaxuallyQueueClient = new Mock<TaxuallyQueueClient>();
            var mockVatRegistrationGB = new Mock<VatRegistrationGB>(MockBehavior.Strict, mockTaxuallyHttpClient.Object);
            var mockVatRegistrationFR = new Mock<VatRegistrationFR>(MockBehavior.Strict, mockTaxuallyQueueClient.Object);
            var mockVatRegistrationDE = new Mock<VatRegistrationDE>(MockBehavior.Strict, mockTaxuallyQueueClient.Object);

            var serviceProvider = new ServiceCollection()
                .AddScoped(provider => mockTaxuallyHttpClient.Object)
                .AddScoped(provider => mockTaxuallyQueueClient.Object)
                .AddScoped(provider => mockVatRegistrationGB.Object)
                .AddScoped(provider => mockVatRegistrationFR.Object)
                .AddScoped(provider => mockVatRegistrationDE.Object)
                .BuildServiceProvider();
            var factory = new VatRegistrationFactory(serviceProvider);

            // Act
            var vatRegistration = factory.GetVatRegister(country);

            // Assert
            Assert.IsInstanceOfType(vatRegistration, typeof(IVatRegistration));
            Assert.IsInstanceOfType(vatRegistration, expectedIVatRegistrationImplementation);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("HU")]
        public void TestGetVatRegistration_EmptyOrUnknownCountry_ThrowsException(string country)
        {
            // Arrange
            var serviceProvider = new ServiceCollection().BuildServiceProvider();
            var factory = new VatRegistrationFactory(serviceProvider);

            try
            {
                // Act
                var vatRegistration = factory.GetVatRegister(country);
            } catch (Exception ex) {
                // Assert
                Assert.AreEqual("Country not supported", ex.Message);
            }
        }
    }
}