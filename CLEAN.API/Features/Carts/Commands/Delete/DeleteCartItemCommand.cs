using MediatR;

namespace CLEAN.API.Features.Carts.Commands.Delete
{
    public record DeleteCartItemCommand(int CartItemID) : IRequest;
}

