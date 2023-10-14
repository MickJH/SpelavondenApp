using System;
using Xunit;
using Portal.Models;

namespace Portal.Tests.Models
{
    public class AgeValidationAttributeTests
    {
        [Fact]
        public void AgeValidationAttribute_ValidateValidAge()
        {
            // Arrange
            var ageValidationAttribute = new AgeValidationAttribute();
            var validBirthDate = new DateTime(2005, 1, 1);

            // Act
            var isValid = ageValidationAttribute.IsValid(validBirthDate);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void AgeValidationAttribute_ValidateInvalidAge()
        {
            // Arrange
            var ageValidationAttribute = new AgeValidationAttribute();
            var invalidBirthDate = new DateTime(2008, 1, 1);

            // Act
            var isValid = ageValidationAttribute.IsValid(invalidBirthDate);

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void AgeValidationAttribute_NullValue_ShouldBeValid()
        {
            // Arrange
            var ageValidationAttribute = new AgeValidationAttribute();

            // Act
            var isValid = ageValidationAttribute.IsValid(null);

            // Assert
            Assert.True(isValid);
        }
    }
}
