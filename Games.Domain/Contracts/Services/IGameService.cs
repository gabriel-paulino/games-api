using Games.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games.Domain.Contracts.Services
{
    public interface IGameService : IDisposable
    {
        Task<IEnumerable<Game>> Get(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<Game> Create(Game game);
        Task<Game> Update(Guid id, Game game);
        Task<Game> UpdatePrice(Guid id, decimal price);
        Task<(bool sucess, string message)> Delete(Guid id);
    }
}