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
        public async Task<IActionResult> CreateBoards([FromBody] CreateBoardCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetBoards([FromQuery] GetBoardsQuery query )
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoard(Guid id)
        {
            return Ok(await Mediator.Send(new GetBoardQuery(){BoardId=id}));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBoard([FromBody] UpdateBoardCommand command )
        {
            await Mediator.Send(command);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveBoard([FromBody] RemoveBoardCommand command )
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}