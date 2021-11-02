using Games.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games.Domain.Contracts.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<IEnumerable<Game>> GetAll(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<IEnumerable<Game>> Get(string name, string producer);
        Task<Game> Create(Game game);
        Task<Game> Update(Game game);
        Task Delete(Guid id);
    }
}