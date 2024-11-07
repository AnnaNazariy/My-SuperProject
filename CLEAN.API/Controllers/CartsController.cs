using CLEAN.API.Domain;
using CLEAN.API.Features.Carts.Commands.Create;
using CLEAN.API.Features.Carts.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CLEAN.API.Features.Carts
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/carts
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCart(CreateCartCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateCart), new { id = result }, result);
        }

        // GET: api/carts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(Guid id)
        {
            var query = new GetCartQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
