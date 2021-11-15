using Games.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Games.Domain.ViewModel
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }
        public int PlatformId { get; set; }
        public int GenreId { get; set; }

        public static implicit operator GameViewModel(Game game) =>
            game is null ? default
            : new()
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price,
                PlatformId = (int)game.Platform,
                GenreId = (int)game.Genre,
            };

        public static IEnumerable<GameViewModel> MapMany(IEnumerable<Game> games)
        {
            var gamesOutput = new List<GameViewModel>();

            foreach (var game in games)
                gamesOutput.Add(game);

            return gamesOutput;
        }
    }
}