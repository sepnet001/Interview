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
    public class BasicTariffTests
    {
        private const double HardcodedConsumption = 500;
        private ITariff _tariff;
        private readonly Mock<ITariffValidator> _mockValidator = new Mock<ITariffValidator>();

        [TestMethod]
        public void CalculateSingleShouldSuccess()
        {
            // Arrange
            _mockValidator.Setup(x => x.Validate(HardcodedConsumption));
            _tariff = new BasicTariff(_mockValidator.Object);

            // Act
            var result = _tariff.Calculate(HardcodedConsumption);

            // Assert
            _mockValidator.Verify(x => x.Validate(It.Is<double>(i => i == HardcodedConsumption)), Times.Once);
            Assert.AreEqual(result.Consumption, HardcodedConsumption);
            Assert.AreEqual(result.TariffType, TariffType.BasicTariff);
            Assert.AreEqual(result.AnnualCost, (5 * 12) + (0.22M * (decimal)HardcodedConsumption));
        }

        [TestMethod]
        public void CalculateShouldSuccess()
        {
            // Arrange
            _mockValidator.Setup(x => x.Validate(HardcodedConsumption));
            _tariff = new BasicTariff(_mockValidator.Object);

            // Act
            var result = _tariff.Calculate(new double[] { HardcodedConsumption });

            // Assert
            _mockValidator.Verify(x => x.Validate(It.Is<double>(i => i == HardcodedConsumption)), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 1);

            var first = result.First();
            Assert.AreEqual(first.Consumption, HardcodedConsumption);
            Assert.AreEqual(first.TariffType, TariffType.BasicTariff);
            Assert.AreEqual(first.AnnualCost, (5 * 12) + (0.22M * (decimal)HardcodedConsumption));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateSingleShouldFail()
        {
            // Arrange
            _mockValidator.Setup(x => x.Validate(HardcodedConsumption)).Throws(new ArgumentException());
            _tariff = new BasicTariff(new TariffValidator());

            // Act
            var result = _tariff.Calculate(new double[] { 0 });

            // Assert - Expects Exception
        }
    }
}
