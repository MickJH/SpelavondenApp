using System;
using System.Linq;
using Xunit;
using Core.Domain.Entities;

namespace Core.Domain.Tests
{
    public class PersonTests
    {
        [Fact]
        public void Person_Name_Required()
        {
            var person = new Person
            {
                Name = null,
                Gender = "Male",
                Address = "1234 Elm St",
                Birthdate = DateTime.Parse("2000-01-01"),
                HasLactoseAllergy = false,
                HasNutAllergy = false,
                IsVegetarian = false,
                AvoidsAlcohol = false
            };

            var result = TestHelper.ValidateModel(person).Any(x => x.ErrorMessage == "Naam is verplicht.");
            Assert.True(result);
        }

        [Fact]
        public void Person_Gender_Required()
        {
            var person = new Person
            {
                Name = "John Doe",
                Gender = null,
                Address = "1234 Elm St",
                Birthdate = DateTime.Parse("2000-01-01"),
                HasLactoseAllergy = false,
                HasNutAllergy = false,
                IsVegetarian = false,
                AvoidsAlcohol = false
            };

            var result = TestHelper.ValidateModel(person).Any(x => x.ErrorMessage == "Gender is verplicht.");
            Assert.True(result);
        }

        [Fact]
        public void Person_Address_Required()
        {
            var person = new Person
            {
                Name = "John Doe",
                Gender = "Male",
                Address = null,
                Birthdate = DateTime.Parse("2000-01-01"),
                HasLactoseAllergy = false,
                HasNutAllergy = false,
                IsVegetarian = false,
                AvoidsAlcohol = false
            };

            var result = TestHelper.ValidateModel(person).Any(x => x.ErrorMessage == "Adres is verplicht.");
            Assert.True(result);
        }

        [Fact]
        public void Person_Birthdate_Required()
        {
            var person = new Person
            {
                Name = "John Doe",
                Gender = "Male",
                Address = "1234 Elm St",
                Birthdate = null,
                HasLactoseAllergy = false,
                HasNutAllergy = false,
                IsVegetarian = false,
                AvoidsAlcohol = false
            };

            var result = TestHelper.ValidateModel(person).Any(x => x.ErrorMessage == "Geboortedatum is verplicht.");
            Assert.True(result);
        }
    }
}
