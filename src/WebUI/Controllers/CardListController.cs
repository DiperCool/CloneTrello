using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.ListsCards.Command.CreateListCard;
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
        
    }
}