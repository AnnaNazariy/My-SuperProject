 
namespace ShopDap.Entities
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public int OrderID { get; set; } 
        public int UserID { get; set; } 
        public decimal TotalAmount { get; set; } 
        public required string Status { get; set; } 
    }


}
