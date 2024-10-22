using MediatR;

namespace CLEAN.API.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest; 
}
