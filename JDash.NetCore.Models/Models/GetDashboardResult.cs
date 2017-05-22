using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Models
{
    public class GetDashboardResult
    {
        public DashboardModel dashboard { get; set; }
        public IEnumerable<DashletModel> dashlets { get; set; }
    }
}
