using CLEAN.API.Features.Carts.DTOs;
using CLEAN.API.Features.Carts.Queries.Get;
using CLEAN.API.Persistence;
using CLEAN.API.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLEAN.API.Features.Carts.Queries.Get
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto?>
    {
        private readonly AppDbContext _context;

        public GetCartQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartDto?> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            // Отримуємо кошик за ID
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CartID == request.CartID, cancellationToken);

            if (cart == null) return null;

            // Повертаємо DTO об'єкт кошика з елементами
            return new CartDto(cart.CartID, cart.UserID, cart.CartItems.Select(ci => new CartItemDto(ci.CartItemID, ci.ProductName, ci.Price, ci.Quantity)).ToList());
        }
    }
}
