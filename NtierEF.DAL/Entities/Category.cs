using System.Collections.Generic;

namespace NtierEF.DAL.Entities
{
    public class Category
    {
        public int CategoryID { get; set; } 
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; } 
    }
}
