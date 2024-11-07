namespace ShopDap.Entities
{
    public class OrderWithUserDetails
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        // Властивості користувача
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
