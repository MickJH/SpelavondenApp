using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Portal.Models;

namespace Portal.Tests.Models
{
    public class RegisterViewModelTests
    {
        [Fact]
        public void RegisterViewModel_ValidData_ShouldPassValidation()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Gender = "Male",
                Address = "1234 Street Ave",
                BirthDate = DateTime.Now.AddYears(-20),
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                HasLactoseAllergy = false,
                HasNutAllergy = false,
                IsVegetarian = true,
                AvoidsAlcohol = false
            };

            // Act
            var validationContext = new ValidationContext(registerViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(registerViewModel, validationContext, validationResults, true);

            // Assert
            Assert.Empty(validationResults);
        }

        [Fact]
        public void RegisterViewModel_InvalidEmail_ShouldFailValidation()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Name = "John Doe",
                Email = "invalidemail", // Invalid email
                Gender = "Male",
                Address = "1234 Street Ave",
                BirthDate = DateTime.Now.AddYears(-20),
                Password = "Password123",
                ConfirmPassword = "Password123",
                HasLactoseAllergy = false,
                HasNutAllergy = false,
                IsVegetarian = true,
                AvoidsAlcohol = false
            };

            // Act
            var validationContext = new ValidationContext(registerViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(registerViewModel, validationContext, validationResults, true);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Email"));
        }

        [Fact]
        public void RegisterViewModel_PasswordMismatch_ShouldFailValidation()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Gender = "Male",
                Address = "1234 Street Ave",
                BirthDate = DateTime.Now.AddYears(-20),
                Password = "Password123",
                ConfirmPassword = "MismatchedPassword", // Mismatched password
                HasLactoseAllergy = false,
                HasNutAllergy = false,
                IsVegetarian = true,
                AvoidsAlcohol = false
            };

            // Act
            var validationContext = new ValidationContext(registerViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(registerViewModel, validationContext, validationResults, true);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("ConfirmPassword"));
        }

        [Fact]
        public void RegisterViewModel_TooYoung_ShouldFailValidation()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Gender = "Male",
                Address = "1234 Street Ave",
                BirthDate = DateTime.Now.AddYears(-15), // Too young (under 16)
                Password = "Password123",
                ConfirmPassword = "Password123",
                HasLactoseAllergy = false,
                HasNutAllergy = false,
                IsVegetarian = true,
                AvoidsAlcohol = false
            };

            // Act
            var validationContext = new ValidationContext(registerViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(registerViewModel, validationContext, validationResults, true);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("BirthDate"));
        }

        [Fact]
        public void RegisterViewModel_InvalidGender_ShouldFailValidation()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Gender = null, // Invalid gender
                Address = "1234 Street Ave",
                BirthDate = DateTime.Now.AddYears(-20),
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                HasLactoseAllergy = false,
                HasNutAllergy = false,
                IsVegetarian = true,
                AvoidsAlcohol = false
            };

            // Act
            var validationContext = new ValidationContext(registerViewModel, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(registerViewModel, validationContext, validationResults, true);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Gender"));
        }
    }
}

