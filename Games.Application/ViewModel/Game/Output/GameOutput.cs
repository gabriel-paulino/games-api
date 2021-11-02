using Games.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Games.Application.ViewModel.Output
{
    public class GameOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }
        public int PlatformId { get; set; }
        public int GenreId { get; set; }

        public static implicit operator GameOutput(Game game) =>
            new()
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price,
                PlatformId = (int)game.Platform,
                GenreId = (int)game.Genre,
            };

        public static IEnumerable<GameOutput> MapMany(IEnumerable<Game> games)
        {
            var gamesOutput = new List<GameOutput>();

            foreach (var game in games)
                gamesOutput.Add(game);

            return gamesOutput;
        }
    }
}