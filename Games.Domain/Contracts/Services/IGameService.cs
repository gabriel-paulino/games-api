using Games.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games.Domain.Contracts.Services
{
    interface IGameService : IDisposable
    {
        Task<IEnumerable<Game>> GetAll(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<Game> Create(Game jogo);
        Task<Game> Update(Guid id, Game jogo);
        Task<Game> UpdatePrice(Guid id, decimal price);
        Task Delete(Guid id);
    }
}