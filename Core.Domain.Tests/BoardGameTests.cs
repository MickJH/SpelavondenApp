using System.Linq;
using Xunit;
using Core.Domain.Entities;
using Core.Domain.Entities.Enums;

namespace Core.Domain.Tests
{
    public class BoardGameTests
    {
        [Fact]
        public void BoardGame_Name_Required()
        {
            var boardGame = new BoardGame
            {
                Name = null,
                Description = "Sample description",
                Genre = GenreType.FANTASIE,
                Is18Plus = false,
                PhotoUrl = "https://example.com",
                GameType = new GameType()
            };

            var result = TestHelper.ValidateModel(boardGame).Any(x => x.ErrorMessage == "Naam is verplicht.");
            Assert.True(result);
        }

        [Fact]
        public void BoardGame_Description_Required()
        {
            var boardGame = new BoardGame
            {
                Name = "Sample Name",
                Description = null,
                Genre = GenreType.FANTASIE,
                Is18Plus = false,
                PhotoUrl = "https://example.com",
                GameType = new GameType()
            };

            var result = TestHelper.ValidateModel(boardGame).Any(x => x.ErrorMessage == "Beschrijving is verplicht.");
            Assert.True(result);
        }

        [Fact]
        public void BoardGame_Genre_Required()
        {
            var boardGame = new BoardGame
            {
                // Populate other required properties
                Name = "Sample Name",
                Description = "Sample description",
                Genre = null,
                Is18Plus = false,
                PhotoUrl = "https://example.com",
                GameType = new GameType()
            };

            var result = TestHelper.ValidateModel(boardGame).Any(x => x.ErrorMessage == "Genre is verplicht.");
            Assert.True(result);
        }

        [Fact]
        public void BoardGame_GameType_Required()
        {
            var boardGame = new BoardGame
            {
                // Populate other required properties
                Name = "Sample Name",
                Description = "Sample description",
                Genre = GenreType.FANTASIE,
                Is18Plus = false,
                PhotoUrl = "https://example.com",
                GameType = null
            };

            var result = TestHelper.ValidateModel(boardGame).Any(x => x.ErrorMessage == "Speltype is verplicht.");
            Assert.True(result);
        }
    }
}
