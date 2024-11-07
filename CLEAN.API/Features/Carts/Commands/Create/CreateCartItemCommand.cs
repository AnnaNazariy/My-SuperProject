using MediatR;

namespace CLEAN.API.Features.Carts.Commands.Create
{
    public record CreateCartItemCommand(int CartID, int ProductID, int Quantity) : IRequest<int>;
}
