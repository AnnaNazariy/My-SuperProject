/*using CLEAN.API.Domain;
using CLEAN.API.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CLEAN.API.Features.Carts.Commands.Create
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, int> 
    {
        private readonly AppDbContext _context;

        public CreateCartCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
            
            var cart = new Cart(command.UserID, "New Cart");

            
            await _context.Carts.AddAsync(cart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return cart.CartID; 
        }
    }
}
*/