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

        public Game(string name, string producer, decimal price, EPlatform platform = EPlatform.Uninformed, EGenre genre = EGenre.Uninformed)
        {
            Name = name;
            Producer = producer;
            Price = price;
            Platform = platform;
            Genre = genre;

            DoValidations();
        }

        public string Name { get; private set; }
        public string Producer { get; private set; }
        public decimal Price { get; private set; }
        public EPlatform Platform { get; private set; }
        public EGenre Genre { get; private set; }


        public Game Update(Game updatedGame)
        {
            Name = updatedGame.Name;
            Producer = updatedGame.Producer;
            Price = updatedGame.Price;
            Platform = updatedGame.Platform;
            Genre = updatedGame.Genre;

            DoValidations();

            return this;
        }

        public void SetPrice(decimal price) 
        { 
            Price = price;
            DoValidations();
        }

        public override void DoValidations()
        {
            ValidatePrice();
        }

        private void ValidatePrice()
        {
            if (Price <= 0 || Price > 1000)
                AddNotification($"Preço inválido informado para o jogo '{Name}'.");
        }
    }
}
