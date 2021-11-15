using Games.Domain.Contracts.Services;
using Games.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Api.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        ///  Get games using pages
        /// </summary>
        /// <param name="page">Quantity of pages.</param>
        /// <param name="quantity">Quantity of lines per page.</param>
        /// <returns></returns>        
        [HttpGet]
        [SwaggerResponse(statusCode: 200, description: "Games", Type = typeof(GameViewModel))]
        [SwaggerResponse(statusCode: 204)]
        [Route("api/[controller]")]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames(
            [FromQuery, Range(1, int.MaxValue)] int page = 1,
            [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Get(page, quantity);

            if (games.Any())
                return Ok(games);

            return NoContent();
        }

        /// <summary>
        ///  Get a Game
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [SwaggerResponse(statusCode: 200, description: "Games", Type = typeof(GameViewModel))]
        [SwaggerResponse(statusCode: 204)]
        [Route("api/[controller]/{id:guid}")]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromRoute] Guid id)
        {
            var game = await _gameService.Get(id);

            if (game is null)
                return NoContent();

            return Ok(game);
        }

        /// <summary>
        /// Create a Game
        /// </summary>
        /// <param name="model">Game Input</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(statusCode: 201, Type = typeof(GameViewModel))]
        [SwaggerResponse(statusCode: 400, Type = typeof(IEnumerable<string>))]
        [Route("api/[controller]")]
        public async Task<ActionResult<GameViewModel>> Create([FromBody] CreateGameViewModel model)
        {
            var game = await _gameService.Create(model);

            return game is null
                ? BadRequest()
                : Created($@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{game.Id}", game);
        }


        /// <summary>
        /// Update a Game
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <param name="model">Game Inputs</param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(statusCode: 200, Type = typeof(GameViewModel))]
        [SwaggerResponse(statusCode: 400, Type = typeof(IEnumerable<string>))]
        [Route("api/[controller]/{id:guid}")]
        public async Task<ActionResult<GameViewModel>> Update([FromRoute] Guid id, [FromBody] UpdateGameViewModel model)
        {
            var game = await _gameService.Update(id, model);

            return game is null
                ? BadRequest()
                : Ok(game);
        }

        /// <summary>
        /// Update only the price of a specific Game
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <param name="price">New Price</param>
        /// <returns></returns>
        [HttpPatch]
        [SwaggerResponse(statusCode: 200, Type = typeof(GameViewModel))]
        [SwaggerResponse(statusCode: 400, Type = typeof(IEnumerable<string>))]
        [Route("api/[controller]/{id:guid}/price/{price:decimal}")]
        public async Task<ActionResult<GameViewModel>> UpdatePrice([FromRoute] Guid id, [FromRoute] decimal price)
        {
            var game = await _gameService.UpdatePrice(id, price);

            return game is null
            ? BadRequest()
            : Ok(game);
        }

        /// <summary>
        /// Delete a Game
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerResponse(statusCode: 200)]
        [SwaggerResponse(statusCode: 400, Type = typeof(string))]
        [SwaggerResponse(statusCode: 404, Type = typeof(string))]
        [Route("api/[controller]/{id:guid}")]
        public async Task<ActionResult<GameViewModel>> Delete([FromRoute] Guid id)
        {
            var (sucess, message) = await _gameService.Delete(id);

            if (sucess)
                return Ok();

            return string.IsNullOrEmpty(message)
                ? BadRequest("Falha ao deletar jogo")
                : NotFound(message);
        }
    }
}