using MediatR;
using CLEAN.API.Features.Carts.DTOs;
using System;

namespace CLEAN.API.Features.Carts.Queries.Get
{
    public record GetCartQuery(Guid CartID) : IRequest<CartDto>;
}
