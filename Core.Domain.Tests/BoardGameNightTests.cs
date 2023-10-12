using System;
using System.Linq;
using Xunit;
using Core.Domain.Entities;
using Core.Domain.Entities.Enums;

namespace Core.Domain.Tests
{
    public class BoardGameNightTests
    {
        [Fact]
        public void BoardGameNight_Address_Required()
        {
            var boardGameNight = new BoardGameNight
            {
                Address = null,
                DateAndTime = DateTime.Now,
                MaxPlayers = 10,
                SelectedBoardGame = new BoardGame(),
                SelectedBoardGameId = 1,
                Games = new List<BoardGame>(),
                FoodAndDrinkOptions = new FoodAndDrinkOption(),
                Snacks = new List<Snacks>(),
                BringSnacks = true
            };

            var result = TestHelper.ValidateModel(boardGameNight).Any(x => x.ErrorMessage == "Adres is verplicht.");
            Assert.True(result);
        }

        [Fact]
        public void BoardGameNight_DateAndTime_Required()
        {
            var boardGameNight = new BoardGameNight
            {
                Address = "Europalaan 20, 3526 KS Utrecht",
                DateAndTime = null,
                MaxPlayers = 10,
                SelectedBoardGame = new BoardGame(),
                SelectedBoardGameId = 1,
                Games = new List<BoardGame>(),
                FoodAndDrinkOptions = new FoodAndDrinkOption(),
                Snacks = new List<Snacks>(),
                BringSnacks = true
            };

            var result = TestHelper.ValidateModel(boardGameNight).Any(x => x.ErrorMessage == "Datum en tijd is verplicht.");
            Assert.True(result);
        }

        [Fact]
        public void BoardGameNight_MaxPlayers_Required()
        {
            var boardGameNight = new BoardGameNight
            {
                Address = "Europalaan 20, 3526 KS Utrecht",
                DateAndTime = DateTime.Now,
                MaxPlayers = null,
                SelectedBoardGame = new BoardGame(),
                SelectedBoardGameId = 1,
                Games = new List<BoardGame>(),
                FoodAndDrinkOptions = new FoodAndDrinkOption(),
                Snacks = new List<Snacks>(),
                BringSnacks = true
            };

            var result = TestHelper.ValidateModel(boardGameNight).Any(x => x.ErrorMessage == "Max aantal spelers is verplicht.");
            Assert.True(result);
        }

        [Fact]
        public void BoardGameNight_SelectedBoardGame_Required()
        {
            var boardGameNight = new BoardGameNight
            {
                Address = "Europalaan 20, 3526 KS Utrecht",
                DateAndTime = DateTime.Now,
                MaxPlayers = 10,
                SelectedBoardGame = null,
                SelectedBoardGameId = 1,
                Games = new List<BoardGame>(),
                FoodAndDrinkOptions = new FoodAndDrinkOption(),
                Snacks = new List<Snacks>(),
                BringSnacks = true
            };

            var result = TestHelper.ValidateModel(boardGameNight).Any(x => x.ErrorMessage == "Spel is verplicht.");
            Assert.True(result);
        }
    }
}
