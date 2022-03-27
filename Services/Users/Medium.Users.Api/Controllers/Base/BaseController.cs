using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Medium.Users.Api.Controllers.Base
{
    [ApiController]
    [Route("api/{v:apiVersion}/[controller]")]
    public class BaseController : Controller
    {
        private IMediator mediator;

        public IMediator Mediator => mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        public Guid UserId => Guid.Parse(HttpContext.User.Identity.Name);
    }
}
