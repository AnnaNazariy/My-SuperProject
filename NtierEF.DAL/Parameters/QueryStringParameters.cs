using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierEF.DAL.Parameters
{
    public class QueryStringParameters
    {
        protected const int MaxPageSize = 50;

        private int pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;

            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? SearchTerm { get; set; }

        public string OrderBy { get; set; } = "name"; 

        public bool IsAscending { get; set; } = true; 
    }
}
