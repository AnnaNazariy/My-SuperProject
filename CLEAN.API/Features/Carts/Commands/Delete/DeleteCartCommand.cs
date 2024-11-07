using MediatR;

namespace CLEAN.API.Features.Carts.Commands.Delete
{
    public record DeleteCartCommand(Guid Id) : IRequest; 
}
