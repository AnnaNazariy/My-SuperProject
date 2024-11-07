using CLEAN.API.Features.Carts.DTOs;
using CLEAN.API.Features.Carts.Queries.Get;
using CLEAN.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLEAN.API.Features.Carts.Queries.Get
{
    public class GetCartItemQueryHandler : IRequestHandler<GetCartItemQuery, CartItemDto?>
    {
        private readonly AppDbContext _context;

        public GetCartItemQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartItemDto?> Handle(GetCartItemQuery request, CancellationToken cancellationToken)
        {
            // Отримуємо елемент кошика за ID
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartItemID == request.CartItemID, cancellationToken);

            if (cartItem == null) return null;

            // Повертаємо DTO об'єкт
            return new CartItemDto(cartItem.CartItemID, cartItem.ProductName, cartItem.Price, cartItem.Quantity);
        }
    }
}
