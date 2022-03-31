using Medium.Desings.Application.Handlers.Desings.Commands.UpdateDesing;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Medium.Desings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesingsController : Controller
    {
        [HttpPost]
        public ActionResult Update([FromForm] UpdateDesingCommand command)
        {
            Console.WriteLine(command.HeaderImage.Display);

            return Ok("");
        }
    }
}
