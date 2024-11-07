using CLEAN.API.Features.Carts.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CLEAN.API.Features.Carts.Notifications
{
    public class CartItemCreatedHandler : INotificationHandler<CartItemCreatedNotification>
    {
        private readonly ILogger<CartItemCreatedHandler> _logger;

        public CartItemCreatedHandler(ILogger<CartItemCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CartItemCreatedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling notification for cart item creation with ID: {notification.CartItemId}. Performing related action.");
            return Task.CompletedTask;
        }
    }
}
