using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Models
{
    public class DashletUpdateModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public Dictionary<string, object> configuration { get; set; }
    }
}
