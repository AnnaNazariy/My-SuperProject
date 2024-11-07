namespace CLEAN.API.Domain
{

    public class CartItem
    {
        public Guid CartItemID { get; set; }
        public Guid CartID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Cart Cart { get; set; }

        public CartItem(Guid cartItemID, string productName, int quantity, decimal price, Guid cartID)
        {
            CartItemID = cartItemID;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
            CartID = cartID;
        }

        public CartItem() { }
    }
}
