using Dapper;
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
        private readonly DbSession _session;
        private readonly bool _disposed = false;

        public GameRepository(DbSession session)
        {
            _session = session;
        }

        ~GameRepository() =>
            Dispose();

        public async Task<bool> Create(Game game)
        {
            string command =
                @"
                    INSERT INTO [Game]
                        ([Id], [Name], [Producer], [Price], [Platform], [Genre]) 
                    VALUES 
                        (@Id, @Name, @Producer, @Price, @Platform, @Genre)
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                game.Id,
                game.Name,
                game.Producer,
                game.Price,
                Platform = (int)game.Platform,
                Genre = (int)game.Genre
            });

            return rowsAffected == 1;
        }

        public async Task<bool> Delete(Guid id)
        {
            string command =
                @"
                    DELETE [Game]
                    WHERE 
                        [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new { Id = id }, _session.Transaction);

            return rowsAffected == 1;
        }

        public async Task<Game> Get(Guid id)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Producer],
                    [Price],
                    [Platform],
                    [Genre]
                FROM 
                    [Game]
                WHERE
                    [Id] = @Id
            ";

            return await _session.Connection.QuerySingleOrDefaultAsync<Game>(query, new { Id = id });
        }

        public async Task<IEnumerable<Game>> Get(string name, string producer)
        {
            string query = @"
                SELECT
                    [Id],
                    [Name],
                    [Producer],
                    [Price],
                    [Platform],
                    [Genre]
                FROM 
                    [Game]
                WHERE
                    [Name] = @Name AND
                    [Producer] = @Producer
            ";

            return await _session.Connection.QueryAsync<Game>(query, new { Name = name, Producer = producer });
        }

        public async Task<IEnumerable<Game>> Get(int page, int quantity)
        {
            string query = (@"
                SELECT
                    [Id],
                    [Name],
                    [Producer],
                    [Price],
                    [Platform],
                    [Genre]
                FROM 
                    [Game]
                ORDER BY [Id]
                OFFSET @Page ROWS 
                FETCH NEXT @Quantity ROWS ONLY
            ");

            return await _session.Connection.QueryAsync<Game>(query, new { Page = (page - 1) * quantity, Quantity = quantity });
        }

        public async Task<bool> Update(Game game)
        {
            string command =
                @"
                    UPDATE [Game] 
                    SET
                        [Name] = @Name,
                        [Producer] = @Producer,
                        [Price] = @Price,
                        [Platform] = @Platform,
                        [Genre] = @Genre
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                game.Id,
                game.Name,
                game.Producer,
                game.Price,
                Platform = (int)game.Platform,
                Genre = (int)game.Genre,
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public async Task<bool> UpdatePrice(Game game)
        {
            string command =
                @"
                    UPDATE [Game] 
                    SET
                        [Price] = @Price
                    WHERE [Id] = @Id
                ";

            int rowsAffected = await _session.Connection.ExecuteAsync(command, new
            {
                game.Id,
                game.Price
            },
            _session.Transaction
            );

            return rowsAffected == 1;
        }

        public void Dispose()
        {
            if (!_disposed)
                _session.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}