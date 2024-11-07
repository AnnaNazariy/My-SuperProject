using System;

namespace NtierEF.DAL.Entities
{
    public class Product
    {
        public int ProductId { get; set; } 
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; } 
        public Category Category { get; set; } 
    }
}
