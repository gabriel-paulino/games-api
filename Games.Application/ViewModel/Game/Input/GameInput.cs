using Games.Domain.Entities;
using Games.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Games.Application.ViewModel.Input
{
    public class GameInput
    {
        [Required(ErrorMessage = "O 'Nome' do jogo é obrigatório")]
        [StringLength(maximumLength: 100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A 'Produtora' do jogo é obrigatória")]
        [StringLength(maximumLength: 100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
        public string Producer { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de no mínimo 1 real e no máximo 1000 reais")]
        public decimal Price { get; set; }

        [Range(0, 14)]
        public int PlatformId { get; set; }

        [Range(0, 9)]
        public int GenreId { get; set; }


        public static implicit operator Game(GameInput model) =>
            new(
                name: model.Name,
                producer: model.Producer,
                price: model.Price,
                platform: (EPlatform)model.PlatformId,
                genre: (EGenre)model.GenreId
                );
    }
}