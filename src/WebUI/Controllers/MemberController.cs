using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Members.Command.JoinBoard;
using CleanArchitecture.Application.Members.Command.KickUser;
using CleanArchitecture.Application.Members.Command.LeaveBoard;
using CleanArchitecture.Application.Members.Command.UpdateMember;
using CleanArchitecture.Application.Members.Query.GetMembers;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
    public class MemberController: ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> JoinBoard([FromBody] JoinBoardCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> LeaveBoard([FromBody] LeaveBoardCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> KickUser([FromBody] KickUserCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMember([FromBody] UpdateMemberCommand command )
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetMembers([FromQuery] GetMembersQuery query )
        {
            return Ok(await Mediator.Send(query));
        }
    }
}