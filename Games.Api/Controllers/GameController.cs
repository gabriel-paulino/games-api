using Games.Api.Filters;
using Games.Application.ViewModel.Generic.Output;
using Games.Application.ViewModel.Input;
using Games.Application.ViewModel.Output;
using Games.Domain.Contracts.Services;
using Games.Domain.Entities;
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
        [SwaggerResponse(statusCode: 200, description: "Games", Type = typeof(GameOutput))]
        [SwaggerResponse(statusCode: 204, description: "Games not found")]
        [Route("api/[controller]")]
        public async Task<ActionResult<IEnumerable<GameOutput>>> GetGames(
            [FromQuery, Range(1, int.MaxValue)] int page = 1,
            [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Get(page, quantity);

            if (games.Any())
                return Ok(GameOutput.MapMany(games));

            return NoContent();
        }

        /// <summary>
        /// Create a Game
        /// </summary>
        /// <param name="model">Game Input</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(statusCode: 201, description: "Game Created", Type = typeof(GameOutput))]
        [SwaggerResponse(statusCode: 400, description: "Invalid Entity", Type = typeof(EntityNotificationOutput<Game>))]
        [CustomValidateModelState]
        [Route("api/[controller]")]
        public async Task<ActionResult<GameOutput>> Create(GameInput model)
        {
            var game = await _gameService.Create(model);

            if (!game.Valid)
                return BadRequest(new EntityNotificationOutput<Game>(game));

            return Created(
                $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{game.Id}",
                (GameOutput)game);
        }


        /// <summary>
        /// Update a Game
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <param name="model">Game Input</param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(statusCode: 200, description: "Game", Type = typeof(GameOutput))]
        [SwaggerResponse(statusCode: 404, description: "Not found", Type = typeof(NotFoundOutput<Game>))]
        [SwaggerResponse(statusCode: 400, description: "Invalid Entity", Type = typeof(EntityNotificationOutput<Game>))]
        [CustomValidateModelState]
        [Route("api/[controller]/{id:guid}")]
        public async Task<ActionResult<GameOutput>> Update([FromRoute] Guid id, [FromBody] GameInput model)
        {
            var game = await _gameService.Update(id, model);

            if (game is null)
                return NotFound(new NotFoundOutput<Game>(model));

            if (!game.Valid)
                return BadRequest(new EntityNotificationOutput<Game>(game));

            return Ok((GameOutput)game);
        }

        /// <summary>
        /// Update only the price of a specific Game
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <param name="price">New Price</param>
        /// <returns></returns>
        [HttpPatch]
        [SwaggerResponse(statusCode: 200, description: "Game", Type = typeof(GameOutput))]
        [SwaggerResponse(statusCode: 404, description: "Not found", Type = typeof(NotFoundOutput<Game>))]
        [SwaggerResponse(statusCode: 400, description: "Invalid Entity", Type = typeof(EntityNotificationOutput<Game>))]
        [CustomValidateModelState]
        [Route("api/[controller]/{id:guid}/price/{price:decimal}")]
        public async Task<ActionResult<GameOutput>> UpdatePrice([FromRoute] Guid id, [FromRoute] decimal price)
        {
            var game = await _gameService.UpdatePrice(id, price);

            if (game is null)
                return NotFound("Não existe este Game");

            if (!game.Valid)
                return BadRequest(new EntityNotificationOutput<Game>(game));

            return Ok((GameOutput)game);
        }

        /// <summary>
        /// Delete a Game
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerResponse(statusCode: 200, description: "Sucess")]
        [SwaggerResponse(statusCode: 400, description: "Fail", Type = typeof(string))]
        [SwaggerResponse(statusCode: 404, description: "Not found", Type = typeof(string))]       
        [CustomValidateModelState]
        [Route("api/[controller]/{id:guid}")]
        public async Task<ActionResult<GameOutput>> Delete([FromRoute] Guid id)
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