using JDash.NetCore.Api.Models;
using JDash.NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Api
{
    public interface IJDashController
    {
        ActionResult MyDashboards();
        ActionResult GetDashboard(string id);
        ActionResult CreateDashboard(DashboardCreateModel model);
        ActionResult SearchDashboards(SearchDashboardsModelWithQuery search);
        ActionResult DeleteDashboard(string id);
        ActionResult SaveDashboard(string id, DashboardUpdateModel updateValues);
        ActionResult CreateDashlet(DashletCreateModel model);
        ActionResult DeleteDashlet(string id);
        ActionResult SaveDashlet(string id, DashletUpdateModel updateValues);
    }
}
