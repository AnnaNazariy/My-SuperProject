using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Entities;

namespace Dapper.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreationTime { get; set; } = DateTime.Now;
        public bool IsRowActive { get; set; } = true;
    }
}
