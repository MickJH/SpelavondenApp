using System.Linq;
using Xunit;
using Core.Domain.Entities;

namespace Core.Domain.Tests
{
    public class FoodAndDrinkOptionsTests
    {
        [Fact]
        public void FoodAndDrinkOptions_LactoseFree()
        {
            var foodAndDrinkOptions = new FoodAndDrinkOption
            {
                LactoseFree = true,
                NutFree = false,
                Vegetarian = false,
                NonAlcoholic = false
            };

            Assert.True(foodAndDrinkOptions.LactoseFree);
            Assert.False(foodAndDrinkOptions.NutFree);
            Assert.False(foodAndDrinkOptions.Vegetarian);
            Assert.False(foodAndDrinkOptions.NonAlcoholic);
        }

        [Fact]
        public void FoodAndDrinkOptions_NutFree()
        {
            var foodAndDrinkOptions = new FoodAndDrinkOption
            {
                LactoseFree = false,
                NutFree = true,
                Vegetarian = false,
                NonAlcoholic = false
            };

            Assert.False(foodAndDrinkOptions.LactoseFree);
            Assert.True(foodAndDrinkOptions.NutFree);
            Assert.False(foodAndDrinkOptions.Vegetarian);
            Assert.False(foodAndDrinkOptions.NonAlcoholic);
        }

        [Fact]
        public void FoodAndDrinkOptions_Vegetarian()
        {
            var foodAndDrinkOptions = new FoodAndDrinkOption
            {
                LactoseFree = false,
                NutFree = false,
                Vegetarian = true,
                NonAlcoholic = false
            };

            Assert.False(foodAndDrinkOptions.LactoseFree);
            Assert.False(foodAndDrinkOptions.NutFree);
            Assert.True(foodAndDrinkOptions.Vegetarian);
            Assert.False(foodAndDrinkOptions.NonAlcoholic);
        }

        [Fact]
        public void FoodAndDrinkOptions_NonAlcoholic()
        {
            var foodAndDrinkOptions = new FoodAndDrinkOption
            {
                LactoseFree = false,
                NutFree = false,
                Vegetarian = false,
                NonAlcoholic = true
            };

            Assert.False(foodAndDrinkOptions.LactoseFree);
            Assert.False(foodAndDrinkOptions.NutFree);
            Assert.False(foodAndDrinkOptions.Vegetarian);
            Assert.True(foodAndDrinkOptions.NonAlcoholic);
        }
    }
}
