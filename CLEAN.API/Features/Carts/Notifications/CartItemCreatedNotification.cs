using MediatR;

namespace CLEAN.API.Features.Carts.Notifications
{
    public record CartItemCreatedNotification(Guid CartItemId) : INotification;
}
