
using Core.Domain.Entities;
using Core.DomainServices.Services.Interfaces;

namespace WebApi.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<BoardGameNight>> getBoardGameNights([Service] IBoardGameNightService _boardGameNightService) => await _boardGameNightService.GetAllBoardGameNightsAsync();
    }
}