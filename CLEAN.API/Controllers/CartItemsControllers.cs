using CLEAN.API.Domain;
using CLEAN.API.Features.Carts.Commands.Create;
using CLEAN.API.Features.Carts.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CLEAN.API.Features.CartItems
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/cartitems
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCartItem(CreateCartItemCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateCartItem), new { id = result }, result);
        }

        // GET: api/cartitems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(Guid id)
        {
            var query = new GetCartItemQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
