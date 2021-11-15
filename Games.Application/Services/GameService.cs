using Games.Domain.Contracts.Repositories;
using Games.Domain.Contracts.Services;
using Games.Domain.Contracts.UoW;
using Games.Domain.Entities;
using Games.Domain.Shared.Contexts;
using Games.Domain.ViewModel;
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
        private readonly NotificationContext _notification;

        public GameService(
            IGameRepository gameRepository,
            IUnitOfWork unitOfWork,
            NotificationContext notification)
        {
            _gameRepository = gameRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<GameViewModel> Create(CreateGameViewModel model)
        {
            var game = (Game)model;
            _notification.AddNotifications(game.Notifications);

            if (!_notification.IsValid)
                return default;

            var games = await _gameRepository.Get(game.Name, game.Producer);

            if (games.Any())
            {
                _notification.AddNotification("", "Jogo já cadastrado");
                return default;
            }

            if (await _gameRepository.Create(game))
                return game;

            _notification.AddNotification("", "Falha ao adicionar jogo");
            return default;
        }

        public async Task<(bool sucess, string message)> Delete(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game is null)
                return (false, "Não existe este Jogo");

            _unitOfWork.BeginTransaction();

            if (await _gameRepository.Delete(id))
            {
                _unitOfWork.Commit();
                return (true, string.Empty);
            }

            _unitOfWork.Rollback();
            return (false, string.Empty);
        }

        public async Task<GameViewModel> Get(Guid id) =>
            await _gameRepository.Get(id);

        public async Task<IEnumerable<GameViewModel>> Get(int page, int quantity)
        {
            var games = await _gameRepository.Get(page, quantity);

            return games.Any()
                ? GameViewModel.MapMany(games)
                : default;
        }


        public async Task<GameViewModel> Update(Guid id, UpdateGameViewModel model)
        {
            if (id != model.Id)
            {
                _notification.AddNotification("Id", "Game not found.");
                return default;
            }

            var updatedGame = (Game)model;
            _notification.AddNotifications(updatedGame);

            if (!_notification.IsValid)
                return default;

            _unitOfWork.BeginTransaction();

            if (await _gameRepository.Update(updatedGame))
            {
                _unitOfWork.Commit();
                return updatedGame;
            }

            _unitOfWork.Rollback();

            _notification.AddNotification("", "Falha ao atualizar jogo");
            return default;
        }

        public async Task<GameViewModel> UpdatePrice(Guid id, decimal price)
        {
            var game = await _gameRepository.Get(id);

            if (game is null)
            {
                _notification.AddNotification("Id", "Game not found.");
                return default;
            }

            game.SetPrice(price);

            if (!game.IsValid)
            {
                _notification.AddNotifications(game.Notifications);
                return default;
            }

            _unitOfWork.BeginTransaction();

            if (await _gameRepository.UpdatePrice(game))
            {
                _unitOfWork.Commit();
                return game;
            }

            _unitOfWork.Rollback();
            _notification.AddNotification("", "Falha ao atualizar jogo");
            return default;
        }

        public void Dispose() =>
            _gameRepository.Dispose();
    }
}