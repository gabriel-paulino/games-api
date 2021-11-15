using Games.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games.Domain.Contracts.Services
{
    public interface IGameService : IDisposable
    {
        Task<IEnumerable<GameViewModel>> Get(int page, int quantity);
        Task<GameViewModel> Get(Guid id);
        Task<GameViewModel> Create(CreateGameViewModel game);
        Task<GameViewModel> Update(Guid id, UpdateGameViewModel game);
        Task<GameViewModel> UpdatePrice(Guid id, decimal price);
        Task<(bool sucess, string message)> Delete(Guid id);
    }
}