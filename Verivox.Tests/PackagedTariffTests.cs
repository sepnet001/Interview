using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Verivox.BLL.Models.Enums;
using Verivox.BLL.ServiceContracts;
using Verivox.BLL.Services;

namespace Verivox.Tests
{
    [TestClass]
    public class PackagedTariffTests
    {
        private const double HardcodedConsumption = 4000;
        private ITariff _tariff;
        private readonly Mock<ITariffValidator> _mockValidator = new Mock<ITariffValidator>();

        [TestMethod]
        public void CalculateSingleShouldSuccess()
        {
            // Arrange
            _mockValidator.Setup(x => x.Validate(HardcodedConsumption));
            _tariff = new PackagedTariff(_mockValidator.Object);

            // Act
            var result = _tariff.Calculate(HardcodedConsumption);

            // Assert
            _mockValidator.Verify(x => x.Validate(It.Is<double>(i => i == HardcodedConsumption)), Times.Once);
            Assert.AreEqual(result.Consumption, HardcodedConsumption);
            Assert.AreEqual(result.TariffType, TariffType.PackagedTariff);
            Assert.AreEqual(result.AnnualCost, 800);
        }

        [TestMethod]
        public void CalculateShouldSuccess()
        {
            // Arrange
            _mockValidator.Setup(x => x.Validate(4500));
            _tariff = new PackagedTariff(_mockValidator.Object);

            // Act
            var result = _tariff.Calculate(new double[] { 4500 });

            // Assert
            _mockValidator.Verify(x => x.Validate(It.Is<double>(i => i == 4500)), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 1);

            var first = result.First();
            Assert.AreEqual(first.Consumption, 4500);
            Assert.AreEqual(first.TariffType, TariffType.PackagedTariff);
            Assert.AreEqual(first.AnnualCost, 800 + (500 * 0.3M));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateSingleShouldFail()
        {
            // Arrange
            _mockValidator.Setup(x => x.Validate(HardcodedConsumption)).Throws(new ArgumentException());
            _tariff = new PackagedTariff(new TariffValidator());

            // Act
            var result = _tariff.Calculate(new double[] { 0 });

            // Assert - Expects Exception
        }
    }
}
