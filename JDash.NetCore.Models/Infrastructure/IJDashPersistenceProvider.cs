using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Models
{

    /* 
     * 
     * 
        getDashboard?(appid: string, id: string): Promise<GetDashboardResult>;
        searchDashboards(search: ISearchDashboard, query?: Query): Promise<QueryResult<DashboardModel>>;
        createDashboard(model: DashboardModel): Promise<CreateResult>;
        deleteDashboard(appid: string, id: string): Promise<any>;
        updateDashboard(appid: string, id: string, updateValues: DashboardUpdateModel): Promise<any>;
        createDashlet(model: DashletModel): Promise<CreateResult>;
        searchDashlets(search: ISearchDashlet): Promise<Array<DashletModel>>;
        deleteDashlet(id: string | Array<string>): Promise<any>;
        updateDashlet(id: string, updateValues: DashletUpdateModel): Promise<any>;
     * 
     * */


    public interface IJDashPersistenceProvider : IDisposable
    {
        bool EnsureTablesCreated();
        GetDashboardResult GetDashboard(string appid, string id);
        QueryResult<DashboardModel> SearchDashboards(SearchDashboardModel searchDashboardModel, Query query);
        CreateResult CreateDashboard(DashboardModel model);
        void DeleteDashboard(string appid, string id);
        void UpdateDashboard(string appid, string id, DashboardUpdateModel updateModel);
        CreateResult CreateDashlet(DashletModel model);
        IEnumerable<DashletModel> SearchDashlets(SearchDashletModel model);
        void DeleteDashlet(string id);
        void DeleteDashlet(IEnumerable<string> ids);
        void UpdateDashlet(string id, DashletUpdateModel model);
        DashletModel GetDashlet(string id);

        DashboardModel GetDashboardById(string appid, string id);

    }
}
