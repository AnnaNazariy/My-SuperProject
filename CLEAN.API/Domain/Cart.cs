namespace CLEAN.API.Domain
{
    public class Cart
    {
        public Guid CartID { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string NewColumn { get; set; }
        public List<CartItem> CartItems { get; set; }
       

        public Cart(Guid cartID, string name)
        {
            CartID = cartID;
            Name = name;
            CartItems = new List<CartItem>();
        }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
