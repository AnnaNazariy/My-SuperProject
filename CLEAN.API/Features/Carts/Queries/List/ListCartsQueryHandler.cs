using CLEAN.API.Features.Carts.DTOs;
using CLEAN.API.Features.Carts.Queries.List;
using CLEAN.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLEAN.API.Features.Carts.Queries.List
{
    public class ListCartsQueryHandler : IRequestHandler<ListCartsQuery, List<CartDto>>
    {
        private readonly AppDbContext _context;

        public ListCartsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartDto>> Handle(ListCartsQuery request, CancellationToken cancellationToken)
        {
            // Отримуємо всі кошики разом з елементами
            return await _context.Carts
                .Include(c => c.CartItems)  
                .Select(c => new CartDto(c.CartID, c.UserID, c.CartItems.Select(ci => new CartItemDto(ci.CartItemID, ci.ProductName, ci.Price, ci.Quantity)).ToList()))
                .ToListAsync(cancellationToken);
        }
    }
}
