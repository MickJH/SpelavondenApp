using System.ComponentModel.DataAnnotations;
using Xunit;
using Portal.Models;

namespace Portal.Tests.Models
{
    public class LoginViewModelTests
    {
        [Fact]
        public void LoginViewModel_InvalidEmail_ShouldFailValidation()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "invalidemail", // Invalid email format
                Password = "Password123"
            };

            // Act
            var validationContext = new ValidationContext(loginViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(loginViewModel, validationContext, validationResults, true);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Email"));
        }

        [Fact]
        public void LoginViewModel_NullEmail_ShouldFailValidation()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = null, // Null email
                Password = "Password123"
            };

            // Act
            var validationContext = new ValidationContext(loginViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(loginViewModel, validationContext, validationResults, true);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Email"));
        }

        [Fact]
        public void LoginViewModel_NullPassword_ShouldFailValidation()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "john.doe@example.com",
                Password = null // Null password
            };

            // Act
            var validationContext = new ValidationContext(loginViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(loginViewModel, validationContext, validationResults, true);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Password"));
        }

        [Fact]
        public void LoginViewModel_EmptyEmail_ShouldFailValidation()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "", // Empty email
                Password = "Password123"
            };

            // Act
            var validationContext = new ValidationContext(loginViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(loginViewModel, validationContext, validationResults, true);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Email"));
        }

        [Fact]
        public void LoginViewModel_EmptyPassword_ShouldFailValidation()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "john.doe@example.com",
                Password = "" // Empty password
            };

            // Act
            var validationContext = new ValidationContext(loginViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(loginViewModel, validationContext, validationResults, true);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Password"));
        }

        [Fact]
        public void LoginViewModel_ValidData_ShouldPassValidation()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Email = "john.doe@example.com",
                Password = "Password123"
            };

            // Act
            var validationContext = new ValidationContext(loginViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(loginViewModel, validationContext, validationResults, true);

            // Assert
            Assert.Empty(validationResults);
        }
    }
}
