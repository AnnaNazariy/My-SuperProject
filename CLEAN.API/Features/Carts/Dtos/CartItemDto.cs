using System;

namespace CLEAN.API.Features.Carts.DTOs
{
    public class CartItemDto
    {
        public Guid CartItemID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public CartItemDto(Guid cartItemID, string productName, decimal price, int quantity)
        {
            CartItemID = cartItemID;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }
    }
}
