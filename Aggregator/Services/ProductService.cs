using Aggregator.Models;
using Grpc.Net.Client;
using ProductGrpc;

namespace Aggregator.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductGrpc.ProductGrpcClient _client;

        public ProductService(ProductGrpc.ProductGrpcClient client)
        {
            _client = client;
        }

        public async Task<bool> DoesProductExistAsync(int productId)
        {
            var request = new DoesProductExistRequest { Id = productId };
            var response = await _client.DoesProductExistAsync(request);
            return response.ProductExists;
        }

        public async Task<List<ProductSummary>> GetProductByIdsAsync(List<int> productIds)
        {
            var request = new GetProductByIdsRequest();
            request.Ids.AddRange(productIds);

            var response = await _client.GetProductByIdsAsync(request);

            return response.Products
                .Select(p => new ProductSummary(p.Id, p.Name, p.Description, p.Price, p.ImageUrl))
                .ToList();
        }
    }
}
