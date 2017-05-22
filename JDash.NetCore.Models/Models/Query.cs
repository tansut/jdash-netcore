using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Models
{
    public class Query
    {
        public int limit { get; set; }
        public int? startFrom { get; set; }
    }

}
