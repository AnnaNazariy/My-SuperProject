namespace Aggregator.Models
{
    public class BasketItem
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public double Price { get; }
        public string ImageUrl { get; }
        public int Quantity { get; private set; }

        public BasketItem(int id, string name, string description, double price, string imageUrl, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            Quantity = quantity;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity cannot be negative or zero", nameof(newQuantity));

            Quantity = newQuantity;
        }

        public void AddToQuantity(int additionalQuantity)
        {
            if (additionalQuantity <= 0)
                throw new ArgumentException("Quantity to add must be positive", nameof(additionalQuantity));

            Quantity += additionalQuantity;
        }

        public double Total => Price * Quantity;
    }
}
