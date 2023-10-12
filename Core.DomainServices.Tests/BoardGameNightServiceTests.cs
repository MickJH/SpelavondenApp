using Xunit;
using Moq;
using Core.DomainServices.Services;
using Core.DomainServices.Repositories.Interfaces;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Tests.Services
{
    public class BoardGameNightServiceTests
    {
        [Fact]
        public async Task GetAllBoardGameNightsAsync_ReturnsAllBoardGameNights()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameNightRepository>();
            mockRepository.Setup(repo => repo.GetAllBoardGameNightsAsync())
                .ReturnsAsync(new List<BoardGameNight> { new BoardGameNight(), new BoardGameNight() });

            var service = new BoardGameNightService(mockRepository.Object);

            // Act
            var result = await service.GetAllBoardGameNightsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }


        [Fact]
        public async Task GetBoardGameNightByIdAsync_ReturnsBoardGameNight()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameNightRepository>();
            var expectedBoardGameNight = new BoardGameNight { Id = 1 };
            mockRepository.Setup(repo => repo.GetBoardGameNightByIdAsync(1))
                .ReturnsAsync(expectedBoardGameNight);

            var service = new BoardGameNightService(mockRepository.Object);

            // Act
            var result = await service.GetBoardGameNightByIdAsync(1);

            // Assert
            Assert.Equal(expectedBoardGameNight, result);
        }

        [Fact]
        public async Task CreateBoardGameNightAsync_CreatesBoardGameNight()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameNightRepository>();
            var service = new BoardGameNightService(mockRepository.Object);
            var expectedBoardGameNight = new BoardGameNight
            {
                Address = "Test address",
                MaxPlayers = 4,
                SelectedBoardGameId = 1,
                Games = new List<BoardGame>()
            };

            mockRepository.Setup(repo => repo.CreateBoardGameNightAsync(It.IsAny<BoardGameNight>()))
                          .ReturnsAsync(expectedBoardGameNight);

            // Act
            var result = await service.CreateBoardGameNightAsync(expectedBoardGameNight);

            // Assert
            Assert.Equal(expectedBoardGameNight.Address, result.Address);
            Assert.Equal(expectedBoardGameNight.MaxPlayers, result.MaxPlayers);
            Assert.Equal(expectedBoardGameNight.SelectedBoardGameId, result.SelectedBoardGameId);
            Assert.Equal(expectedBoardGameNight.Games, result.Games);
        }

        [Fact]
        public async Task UpdateBoardGameNightAsync_UpdatesNightBoardGame()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameNightRepository>();
            var service = new BoardGameNightService(mockRepository.Object);
            var boardGameNight = new BoardGameNight { Id = 1, Address = "Old Address" };

            // Set up the mock repository to return a BoardGameNight when GetBoardGameNightByIdAsync is called
            mockRepository.Setup(repo => repo.GetBoardGameNightByIdAsync(1)).ReturnsAsync(boardGameNight);

            // Act
            await service.UpdateBoardGameNightAsync(1, boardGameNight);

            // Assert
            mockRepository.Verify(repo => repo.UpdateBoardGameNightAsync(boardGameNight), Times.Once);
        }

        [Fact]
        public async Task DeleteBoardGameNightAsync_DeletesBoardGameNight()
        {
            // Arrange
            var mockRepository = new Mock<IBoardGameNightRepository>();
            var service = new BoardGameNightService(mockRepository.Object);

            // Act
            await service.DeleteBoardGameNightAsync(1);

            // Assert
            mockRepository.Verify(repo => repo.DeleteBoardGameNightAsync(1), Times.Once);
        }
    }
}
