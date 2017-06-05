using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDash.NetCore.Models;
using JDash.NetCore.Api.Models;

namespace JDash.NetCore.Api
{
    public class JDashApiController : Controller, IJDashController
    {
        private BaseJDashConfigurator _configurator;
        private JDashPrincipal _principal;
        private BaseJDashConfigurator Configurator
        {
            get
            {
                if (_configurator == null)
                {
                    _configurator = (BaseJDashConfigurator)Activator.CreateInstance(Configuration.ConfiguratorType, new object[] { this.HttpContext });
                }
                return _configurator;
            }
        }


        private JDashPrincipal Principal
        {
            get
            {
                if (_principal == null)
                {
                    _principal = Configurator.GetDecryptedPrincipal();
                }
                return _principal;
            }
        }

        [HttpGet("status")]
        public ActionResult status()
        {

            return Ok("api works");
        }


        [HttpPost("dashlet/delete/{id}")]
        public ActionResult DeleteDashlet(string id)
        {
            var principal = this.Principal;
            using (var persistance = this.Configurator._GetProvider())
            {
                var dashlet = persistance.GetDashlet(id);
                if (dashlet == null)
                    return NotFound("Object [dashlet : " + id + "] Not Found To Remove");

                var dashboard = persistance.GetDashboardById(principal.appid, dashlet.dashboardId);
                if (dashboard == null)
                    return NotFound("Object [dashboard : " + dashlet.dashboardId + "] Not Found");

                if (dashboard.user != principal.user && dashboard.appid != principal.appid)
                    return Unauthorized();

                persistance.DeleteDashlet(id);
                return Ok();
            }
        }


        [HttpPost("dashlet/create")]
        public ActionResult CreateDashlet([FromBody] DashletCreateModel model)
        {
            var principal = this.Principal;
            using (var persistance = this.Configurator._GetProvider())
            {

                var dashboard = persistance.GetDashboardById(principal.appid, model.dashboardId);
                if (dashboard == null)
                    return NotFound("Object [dashboard : " + model.dashboardId + "] Not Found");

                if (dashboard.user != principal.user)
                    return Unauthorized();

                var dashletModel = new DashletModel
                {
                    title = model.title,
                    configuration = model.configuration,
                    description = model.description,
                    createdAt = DateTime.UtcNow,
                    dashboardId = model.dashboardId,
                    moduleId = model.moduleId
                };

                var createResult = persistance.CreateDashlet(dashletModel);
                return Ok(createResult);
            }
        }

        [HttpGet("dashboard/my")]
        public ActionResult MyDashboards()
        {
            var principal = this.Principal;
            using (var persistance = this.Configurator._GetProvider())
            {
                var dashboards = persistance.SearchDashboards(new SearchDashboardModel
                {
                    appid = principal.appid,
                    user = principal.user
                }, null);

                return Ok(dashboards);
            }
        }

        [HttpPost("dashboard/delete/{id}")]
        public ActionResult DeleteDashboard(string id)
        {
            var principal = this.Principal;
            using (var persistance = this.Configurator._GetProvider())
            {
                var dashboard = persistance.GetDashboardById(principal.appid, id);
                if (dashboard == null)
                    return NotFound("Object [dashboard : " + id + "] Not Found");

                if (dashboard.user != principal.user)
                    return Unauthorized();

                persistance.DeleteDashboard(principal.appid, id);
                return Ok();

            }
        }

        [HttpGet("dashboard/{id}")]
        public ActionResult GetDashboard(string id)
        {
            var principal = this.Principal;
            using (var persistance = this.Configurator._GetProvider())
            {
                var dashboard = persistance.GetDashboard(principal.appid, id);
                if (dashboard == null || dashboard.dashboard == null)
                    return NotFound("Object [dashboard : " + id + "] Not Found");

                if (dashboard.dashboard.user != principal.user)
                    return Unauthorized();

                return Ok(dashboard);
            }
        }


        [HttpPost("dashboard/create")]
        public ActionResult CreateDashboard([FromBody] DashboardCreateModel model)
        {
            var principal = this.Principal;
            using (var persistance = this.Configurator._GetProvider())
            {
                var newModel = new DashboardModel
                {
                    title = model.title,
                    appid = principal.appid,
                    config = model.config,
                    createdAt = DateTime.UtcNow,
                    description = model.description,
                    layout = model.layout,
                    shareWith = model.shareWith,
                    user = principal.user,
                    id = null
                };

                var result = persistance.CreateDashboard(newModel);
                return Ok(result);
            }
        }


        [HttpPost("dashboard/search")]
        public ActionResult SearchDashboards([FromBody] SearchDashboardsModelWithQuery search)
        {

            // FIXME : Search can be array of strings
            var principal = this.Principal;
            using (var persistance = this.Configurator._GetProvider())
            {

                var searchResult = persistance.SearchDashboards(new SearchDashboardModel
                {
                    appid = principal.appid,
                    user = search.search.user,
                    shareWith = search.search.shareWith
                }, search.query);

                return Ok(searchResult);
            }
        }


        [HttpPost("dashboard/save/{id}")]
        public ActionResult SaveDashboard([FromRoute]string id, [FromBody] DashboardUpdateModel updateValues)
        {
            var principal = this.Principal;
            using (var persistance = this.Configurator._GetProvider())
            {

                var dashboardResult = persistance.GetDashboard(principal.appid, id);
                if (dashboardResult == null || dashboardResult.dashboard == null)
                    return NotFound("Object [dashboard : " + id + "] Not Found");

                if (dashboardResult.dashboard.user != principal.user)
                    return Unauthorized();

                // we need to remove old dashlets if removed on client when saving dashboard.
                if (updateValues.layout != null && updateValues.layout.dashlets != null)
                {
                    if (dashboardResult.dashlets != null)
                    {
                        var oldDashletIds = dashboardResult.dashlets.Select(d => d.id);
                        var newDashletIds = updateValues.layout.dashlets.Keys.Select(d => d.ToString());
                        var removedDashletIds = oldDashletIds.Except(newDashletIds);
                        persistance.DeleteDashlet(removedDashletIds);
                    }
                }

                persistance.UpdateDashboard(principal.appid, id, updateValues);
                return Ok();

            }
        }


        [HttpPost("dashlet/save/{id}")]
        public ActionResult SaveDashlet([FromRoute]string id, [FromBody]DashletUpdateModel updateValues)
        {
            var principal = this.Principal;
            using (var persistance = this.Configurator._GetProvider())
            {
                var dashlet = persistance.GetDashlet(id);
                if (dashlet == null)
                    return NotFound("Object [dashlet : " + id + "] Not Found To Remove");

                var dashboard = persistance.GetDashboardById(principal.appid, dashlet.dashboardId);
                if (dashboard == null)
                    return NotFound("Object [dashboard : " + dashlet.dashboardId + "] Not Found");

                if (dashboard.user != principal.user && dashboard.appid != principal.appid)
                    return Unauthorized();

                persistance.UpdateDashlet(id, updateValues);
                return Ok();
            }
        }

    }
}
