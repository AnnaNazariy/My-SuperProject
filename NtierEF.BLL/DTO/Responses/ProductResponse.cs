using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NtierEF.BLL.DTO.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public required string? ProductName { get; set; }
        public required string? CategoryName { get; set; }
        public decimal Price { get; set; }       
        public int CategoryID { get; set; }          }
}
