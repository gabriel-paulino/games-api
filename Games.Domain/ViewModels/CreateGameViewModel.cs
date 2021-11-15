using Games.Domain.Entities;
using Games.Domain.Enums;

namespace Games.Domain.ViewModel
{
    public class CreateGameViewModel
    {
        public string Name { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }
        public int PlatformId { get; set; }
        public int GenreId { get; set; }

        public static implicit operator Game(CreateGameViewModel model) =>
            new(
                name: model.Name,
                producer: model.Producer,
                price: model.Price,
                platform: (EPlatform)model.PlatformId,
                genre: (EGenre)model.GenreId
                );
    }
}