/*using CLEAN.API.Domain;
using CLEAN.API.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CLEAN.API.Features.Carts.Commands.Create
{
    public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateCartItemCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCartItemCommand command, CancellationToken cancellationToken)
        {
            var cartItem = new CartItemID(command.CartID, command.ProductID, command.Quantity);

            await _context.CartItems.AddAsync(cartItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return cartItem.CartID;
        }
    }
}
*/