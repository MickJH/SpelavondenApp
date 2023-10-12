using Xunit;
using Moq;
using Core.DomainServices.Services;
using Core.DomainServices.Repositories.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Core.Domain.Tests.Services
{
    public class BoardGameServiceTests
    {
        [Fact]
        public async Task GetAllBoardGamesAsync_ReturnsAllBoardGames()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameRepository>();
            mockRepository.Setup(repo => repo.GetAllBoardGamesAsync())
                .ReturnsAsync(new List<BoardGame> { new BoardGame(), new BoardGame() });

            var service = new BoardGameService(mockRepository.Object);

            // Act
            var result = await service.GetAllBoardGamesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetBoardGameByIdAsync_ReturnsBoardGame()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameRepository>();
            var expectedBoardGame = new BoardGame { Id = 1 };
            mockRepository.Setup(repo => repo.GetBoardGameByIdAsync(1))
                .ReturnsAsync(expectedBoardGame);

            var service = new BoardGameService(mockRepository.Object);

            // Act
            var result = await service.GetBoardGameByIdAsync(1);

            // Assert
            Assert.Equal(expectedBoardGame, result);
        }

        [Fact]
        public async Task CreateBoardGameAsync_CreatesBoardGame()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameRepository>();
            var service = new BoardGameService(mockRepository.Object);
            var expectedBoardGame = new BoardGame
            {
                Description = "Test description",
                GameType = new GameType { Type = "Test type" },
                Is18Plus = false,
            };

            mockRepository.Setup(repo => repo.CreateBoardGameAsync(It.IsAny<BoardGame>()))
                          .ReturnsAsync(expectedBoardGame);

            // Act
            var result = await service.CreateBoardGameAsync(expectedBoardGame);

            // Assert
            Assert.Equal(expectedBoardGame.Description, result.Description);
            Assert.Equal(expectedBoardGame.GameType, result.GameType);
            Assert.Equal(expectedBoardGame.Genre, result.Genre);
            Assert.Equal(expectedBoardGame.Is18Plus, result.Is18Plus);
        }


        [Fact]
        public async Task UpdateBoardGameAsync_UpdatesBoardGame()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameRepository>();
            var service = new BoardGameService(mockRepository.Object);
            var boardGame = new BoardGame { Id = 1, Name = "Old Name" };

            // Act
            await service.UpdateBoardGameAsync(1, boardGame);

            // Assert
            mockRepository.Verify(repo => repo.UpdateBoardGameAsync(boardGame), Times.Once);
        }

        [Fact]
        public async Task DeleteBoardGameAsync_DeletesBoardGame()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameRepository>();
            var service = new BoardGameService(mockRepository.Object);

            // Act
            await service.DeleteBoardGameAsync(1);

            // Assert
            mockRepository.Verify(repo => repo.DeleteBoardGameAsync(1), Times.Once);
        }
    }
}
