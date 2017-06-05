using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Models
{
    public class LayoutModel
    {
        public string module { get; set; }

        [CanBeNull]
        public Dictionary<string, object> config { get; set; }

        public Dictionary<string, DashletPositionModel> dashlets { get; set; }

    }
}
