using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Boards.Command.CreateBoard;
using CleanArchitecture.Application.Boards.Command.RemoveBoard;
using CleanArchitecture.Application.Boards.Command.UpdateBoard;
using CleanArchitecture.Application.Boards.Query.GetBoard;
using CleanArchitecture.Application.Boards.Query.GetBoards;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
    public class BoardController: ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBoardCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetBoardsQuery query )
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetBoardQuery(){BoardId=id}));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBoardCommand command )
        {
            await Mediator.Send(command);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody] RemoveBoardCommand command )
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}