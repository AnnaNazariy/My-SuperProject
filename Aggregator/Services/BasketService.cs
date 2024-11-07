using Aggregator.Models;
using Grpc.Net.Client;
using BasketGrpc;

namespace Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly BasketGrpc.BasketGrpcClient _client;

        public BasketService(BasketGrpc.BasketGrpcClient client)
        {
            _client = client;
        }

        public async Task AddProductToBasketAsync(Guid customerId, int productId, int quantity)
        {
            var request = new AddBasketItemRequest
            {
                CustomerId = customerId.ToString(),
                ProductId = productId,
                Quantity = quantity
            };

            await _client.AddBasketItemAsync(request);
        }

        public async Task<List<BasketItemData>> GetBasketItemsAsync(Guid customerId)
        {
            var request = new GetBasketRequest { CustomerId = customerId.ToString() };
            var response = await _client.GetBasketAsync(request);

            return response.Items
                .Select(i => new BasketItemData(i.ProductId, i.Quantity))
                .ToList();
        }
    }
}
