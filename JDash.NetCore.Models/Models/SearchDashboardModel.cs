using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Models
{

    public class SearchDashboardModel
    {
        /// <summary>
        /// Can be ArrayOfString (JSON) or just string
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// Can be ArrayOfString  (JSON) or just string
        /// </summary>
        public string user { get; set; }

        /// <summary>
        /// Can be ArrayOfString  (JSON) or just string
        /// </summary>
        public string shareWith { get; set; }
    }
}
