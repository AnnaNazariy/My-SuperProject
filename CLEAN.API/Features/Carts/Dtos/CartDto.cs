using CLEAN.API.Features.Carts.DTOs;
using System;

namespace CLEAN.API.Features.Carts.DTOs
{
    public class CartDto
    {
        public Guid CartID { get; set; }
        public Guid UserID { get; set; }
        public List<CartItemDto> CartItems { get; set; }

        public CartDto(Guid cartID, Guid userID, List<CartItemDto> cartItems)
        {
            CartID = cartID;
            UserID = userID;
            CartItems = cartItems;
        }
    }

}
