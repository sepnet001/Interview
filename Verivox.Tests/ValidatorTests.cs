using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Verivox.BLL.ServiceContracts;
using Verivox.BLL.Services;

namespace Verivox.Tests
{
    [TestClass]
    public class ValidatorTests
    {
        private ITariffValidator _validator;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateShouldFailByNegativeValue()
        {
            // Arrange
            _validator = new TariffValidator();

            // Act
            _validator.Validate(-1);

            // Assert - Expects exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateShouldFailByZeroValue()
        {
            // Arrange
            _validator = new TariffValidator();

            // Act
            _validator.Validate(0);

            // Assert - Expects exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateShouldSuccess()
        {
            // Arrange
            _validator = new TariffValidator();

            // Act => Assert
            _validator.Validate(0);
        }
    }
}
