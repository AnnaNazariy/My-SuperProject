namespace Aggregator.Models
{
    public record CustomerBasket(Guid BuyerId, List<BasketItem> Items)
    {
        public double Total => Items.Sum(item => item.Total);
    }
}
