using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Models
{
    public class SearchQuery : Query
    {
        public Dictionary<string, object> filters { get; set; }
    }
}
