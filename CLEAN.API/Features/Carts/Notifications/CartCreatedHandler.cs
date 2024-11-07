using CLEAN.API.Features.Carts.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CCLEAN.API.Features.Carts.Notifications
{
    public class CartCreatedHandler : INotificationHandler<CartCreatedNotification>
    {
        private readonly ILogger<CartCreatedHandler> _logger;

        public CartCreatedHandler(ILogger<CartCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CartCreatedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling notification for cart creation with ID: {notification.CartId}. Performing related action.");
            return Task.CompletedTask;
        }
    }
}
