using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductivityBook.API.Common
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
         protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
