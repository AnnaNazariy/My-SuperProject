using MediatR;
using CLEAN.API.Features.Carts.DTOs;

namespace CLEAN.API.Features.Carts.Queries.Get
{
    public record GetCartItemQuery(Guid CartItemID) : IRequest<CartItemDto>;
}
