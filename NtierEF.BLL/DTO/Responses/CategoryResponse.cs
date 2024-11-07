using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierEF.BLL.DTO.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }      
        public required string? ProductName { get; set; }
        public required string? CategoryName { get; set; }
    }
}
