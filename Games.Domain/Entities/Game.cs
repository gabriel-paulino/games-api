using Flunt.Validations;
using Games.Domain.Entities.Base;
using Games.Domain.Enums;
using System;

namespace Games.Domain.Entities
{
    public class Game : BaseEntity
    {
        public Game(Guid id, string name, string producer, decimal price, EPlatform platform, EGenre genre)
        {
            Id = id;
            Name = name;
            Producer = producer;
            Price = price;
            Platform = platform;
            Genre = genre;
        }

        public Game(Guid id, string name, string producer, decimal price, int platformId, int genreId)
        {
            AddNotifications(
                GetContractGame(name, producer, price, (EPlatform)platformId, (EGenre)genreId));

            if (IsValid)
            {
                Id = id;
                Name = name;
                Producer = producer;
                Price = price;
                Platform = (EPlatform)platformId;
                Genre = (EGenre)genreId;
            }              
        }

        public Game(string name, string producer, decimal price, EPlatform platform = EPlatform.Uninformed, EGenre genre = EGenre.Uninformed)
        {
            AddNotifications(GetContractGame(name, producer, price, platform, genre));

            if (IsValid)
            {
                Name = name;
                Producer = producer;
                Price = price;
                Platform = platform;
                Genre = genre;
            }
        }

        public string Name { get; private set; }
        public string Producer { get; private set; }
        public decimal Price { get; private set; }
        public EPlatform Platform { get; private set; }
        public EGenre Genre { get; private set; }

        public void SetPrice(decimal price)
        {
            AddNotifications(GetPriceContract(price));

            if (IsValid)
                Price = price;
        }

        private static Contract<Game> GetContractGame(string name, string producer, decimal price, EPlatform platform, EGenre genre) =>
            new Contract<Game>()
                .IsLowerThan(name, 100, "Name", "Name should have no more than 100 chars")
                .IsGreaterThan(name, 3, "Name", "Name should have at least 3 chars")
                .IsLowerThan(producer, 100, "Producer", "Producer should have no more than 100 chars")
                .IsGreaterThan(producer, 3, "Producer", "Producer should have at least 3 chars")
                .IsBetween((int)platform, 0, 14, "Platform", "PlatformId should have at least 0 and no more than 14")
                .IsBetween((int)genre, 0, 9, "Genre", "GenreId should have at least 0 and no more than 9")
                .Join(GetPriceContract(price));
        
        private static Contract<Game> GetPriceContract(decimal price) =>
            new Contract<Game>()
                .IsBetween(price, 1, 1000, "Price", "Price should have at least R$ 1 and no more than R$ 1000");
    }
}
