using Games.Domain.Contracts.Repositories;
using Games.Domain.Contracts.Services;
using Games.Domain.Contracts.UoW;
using Games.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GameService(
            IGameRepository gameRepository,
            IUnitOfWork unitOfWork)
        {
            _gameRepository = gameRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Game> Create(Game game)
        {
            var games = await _gameRepository.Get(game.Name, game.Producer);

            if (games.Any())
            {
                game.AddNotification("Jogo já cadastrado");
                return game;
            }

            if (await _gameRepository.Create(game))
                return game;

            game.AddNotification("Falha ao adicionar jogo");
            return game;
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Game> Get(Guid id) =>
            await _gameRepository.Get(id);

        public async Task<IEnumerable<Game>> Get(int page, int quantity) =>
            await _gameRepository.Get(page, quantity);

        public async Task<Game> Update(Guid id, Game game)
        {
            throw new NotImplementedException();
        }

        public async Task<Game> UpdatePrice(Guid id, decimal price)
        {
            throw new NotImplementedException();
        }

        public void Dispose() =>
            _gameRepository.Dispose();
    }
}