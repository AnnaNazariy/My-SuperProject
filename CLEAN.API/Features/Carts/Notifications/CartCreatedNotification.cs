using MediatR;

    namespace CLEAN.API.Features.Carts.Notifications
{ 
    
        public record CartCreatedNotification(Guid CartId) : INotification;
    }


