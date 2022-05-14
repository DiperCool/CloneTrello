using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.ListsCards.Command.CreateListCard;
using CleanArchitecture.Application.ListsCards.Command.MoveListCard;
using CleanArchitecture.Application.ListsCards.Command.RemoveListCard;
using CleanArchitecture.Application.ListsCards.Command.UpdateListCard;
using CleanArchitecture.Application.ListsCards.Query.GetListsCards;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
    public class CardListController: ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateListCards([FromBody] CreateListCardsCommand command )
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetListsCards([FromQuery] GetListsCardsQuery query )
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost]
        public async Task<IActionResult> MoveListCard([FromBody] MoveListCardCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateListCard([FromBody] UpdateListCardCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveListCard([FromBody] RemoveListCardCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}