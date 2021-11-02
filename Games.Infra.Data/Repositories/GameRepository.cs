using Games.Domain.Contracts.Repositories;
using Games.Domain.Entities;
using Games.Infra.Data.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games.Infra.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly DbSession _dbSession;
        private bool _disposed = false;

        public GameRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        ~GameRepository() =>
            Dispose();

        public Task<Game> Create(Game game)
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

        public Task<IEnumerable<Game>> Get(string name, string producer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAll(int page, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<Game> Update(Game game)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (!_disposed)
                _dbSession.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}