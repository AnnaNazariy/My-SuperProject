using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierEF.DAL.Parameters
{
        public class ProductParameters : QueryStringParameters
        {
            public string ProductName { get; set; } 
            public int? CategoryID { get; set; } 
        }
    }


