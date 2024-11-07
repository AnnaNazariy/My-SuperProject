using CLEAN.API.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CLEAN.API.Features.Carts.Commands.Delete
{
    public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand>
    {
        private readonly AppDbContext _context;

        public DeleteCartItemCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _context.CartItems.FindAsync(request.CartItemID);
            if (cartItem == null) return Unit.Value; 

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value; 
        }

        Task IRequestHandler<DeleteCartItemCommand>.Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
