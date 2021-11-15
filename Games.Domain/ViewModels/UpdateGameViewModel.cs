using Games.Domain.Entities;
using Games.Domain.Enums;
using System;

namespace Games.Domain.ViewModel
{
    public class UpdateGameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }
        public int PlatformId { get; set; }
        public int GenreId { get; set; }

        public static implicit operator Game(UpdateGameViewModel model) =>
            new(
                id: model.Id,
                name: model.Name,
                producer: model.Producer,
                price: model.Price,
                platformId: model.PlatformId,
                genreId: model.GenreId
                );
    }
}