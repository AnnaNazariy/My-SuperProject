using CLEAN.API.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CLEAN.API.Features.Carts.Commands.Delete
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand>
    {
        private readonly AppDbContext _context;

        public DeleteCartCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts.FindAsync(request.Id);
            if (cart == null) return Unit.Value; 

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value; 
        }

        Task IRequestHandler<DeleteCartCommand>.Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
