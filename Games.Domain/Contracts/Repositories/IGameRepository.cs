using Games.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games.Domain.Contracts.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<IEnumerable<Game>> Get(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<IEnumerable<Game>> Get(string name, string producer);
        Task<bool> Create(Game game);
        Task<bool> Update(Game game);
        Task<bool> Delete(Guid id);
    }
}