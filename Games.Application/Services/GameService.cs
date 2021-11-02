using Games.Domain.Contracts.Repositories;
using Games.Domain.Contracts.Services;
using Games.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<Game> Create(Game jogo)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Game> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAll(int page, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<Game> Update(Guid id, Game jogo)
        {
            throw new NotImplementedException();
        }

        public Task<Game> UpdatePrice(Guid id, decimal price)
        {
            throw new NotImplementedException();
        }

        public void Dispose() => _gameRepository?.Dispose();
    }
}