using CLEAN.API.Features.Carts.DTOs;
using CLEAN.API.Features.Carts.Queries.List;
using CLEAN.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLEAN.API.Features.Carts.Queries.List
{
    public class ListCartItemsQueryHandler : IRequestHandler<ListCartItemsQuery, List<CartItemDto>>
    {
        private readonly AppDbContext _context;

        public ListCartItemsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItemDto>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
        {
            // Отримуємо всі елементи кошика
            return await _context.CartItems
                .Select(ci => new CartItemDto(ci.CartItemID, ci.ProductName, ci.Price, ci.Quantity))
                .ToListAsync(cancellationToken);
        }
    }
}
