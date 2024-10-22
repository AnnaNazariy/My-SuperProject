using MediatR;

namespace NtierCLA.API.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest; 
}
