using MediatR;
using CLEAN.API.Features.Carts.DTOs;

namespace CLEAN.API.Features.Carts.Queries.List
{
    public record ListCartsQuery : IRequest<List<CartDto>>;
}
