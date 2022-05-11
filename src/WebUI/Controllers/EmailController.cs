using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.SendEmail.Command.Send;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.WebUI.Controllers;
public class EmailController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult> SendEmail(SendEmailCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}