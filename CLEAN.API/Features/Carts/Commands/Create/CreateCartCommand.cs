using MediatR;

namespace CLEAN.API.Features.Carts.Commands.Create
{
    public record CreateCartCommand(int UserID) : IRequest<int>;
}
