using MediatR;

namespace dotnet.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest; 
}
