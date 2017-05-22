using JDash.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Api.Models
{
    public class SearchDashboardsModelWithQuery
    {
        public SearchDashboardModel search { get; set; }
        public Query query { get; set; }
    }
}
