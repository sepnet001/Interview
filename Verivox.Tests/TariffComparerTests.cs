using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Verivox.BLL.Models;
using Verivox.BLL.Models.Enums;
using Verivox.BLL.ServiceContracts;
using Verivox.BLL.Services;

namespace Verivox.Tests
{
    [TestClass]
    public class TariffComparerTests
    {
        private readonly Mock<ITariff> _basicTariff = new Mock<ITariff>();
        private readonly Mock<ITariff> _packagedTariff = new Mock<ITariff>();

        private ITariffComparer _tariffComparer;

        [TestMethod]
        public void ShouldExecuteForSingleItemsSuccessFully()
        {
            // Arrange
            _basicTariff.Setup(x => x.Calculate(It.IsAny<double>())).Returns(new Product
            {
                Consumption = 500,
                TariffType = TariffType.BasicTariff,
                AnnualCost = 500
            });
            _packagedTariff.Setup(x => x.Calculate(It.IsAny<double>())).Returns(new Product
            {
                Consumption = 500,
                TariffType = TariffType.PackagedTariff,
                AnnualCost = 500
            });

            _tariffComparer = new TariffComparer(_basicTariff.Object, _packagedTariff.Object);

            // Act
            _tariffComparer.Compare(new double[] { 500 });

            // Assert
            _basicTariff.Verify(x => x.Calculate(It.IsAny<double>()), Times.Once);
            _packagedTariff.Verify(x => x.Calculate(It.IsAny<double>()), Times.Once);
        }

        [TestMethod]
        public void ShouldExecuteForMultipleItemsSuccessFully()
        {
            // Arrange
            _basicTariff.Setup(x => x.Calculate(It.IsAny<double>())).Returns(new Product
            {
                Consumption = 500,
                TariffType = TariffType.BasicTariff,
                AnnualCost = 500
            });
            _packagedTariff.Setup(x => x.Calculate(It.IsAny<double>())).Returns(new Product
            {
                Consumption = 500,
                TariffType = TariffType.PackagedTariff,
                AnnualCost = 500
            });

            _tariffComparer = new TariffComparer(_basicTariff.Object, _packagedTariff.Object);

            // Act
            _tariffComparer.Compare(new double[] { 500, 800 });

            // Assert
            _basicTariff.Verify(x => x.Calculate(It.IsAny<double>()), Times.Exactly(2));
            _packagedTariff.Verify(x => x.Calculate(It.IsAny<double>()), Times.Exactly(2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowException()
        {
            // Arrange
            _basicTariff.Setup(x => x.Calculate(It.IsAny<double>())).Throws(new ArgumentException());
            _packagedTariff.Setup(x => x.Calculate(It.IsAny<double>())).Throws(new ArgumentException());

            _tariffComparer = new TariffComparer(_basicTariff.Object, _packagedTariff.Object);

            // Act
            _tariffComparer.Compare(new double[] { 500, 800 });

            // Assert - Expects Exception
        }
    }
}
