using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Cards.Command.CreateCard;
using CleanArchitecture.Application.Cards.Command.MoveCard;
using CleanArchitecture.Application.Cards.Command.RemoveCard;
using CleanArchitecture.Application.Cards.Command.UpdateCard;
using CleanArchitecture.Application.Cards.Query.GetCard;
using CleanArchitecture.Application.Cards.Query.GetCards;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
    public class CardController:ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCard([FromBody] UpdateCardCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCard([FromBody] RemoveCardCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetCards([FromQuery] GetCardsQuery query )
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCard(Guid id)
        {
            return Ok(await Mediator.Send(new GetCardQuery{CardId=id}));
        }
        [HttpPost]
        public async Task<IActionResult> MoveCard([FromBody] MoveCardCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
    }
}