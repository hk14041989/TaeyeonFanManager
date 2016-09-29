using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;


namespace TaeyeonFanManagerSite.DAL
{
    public class TeamConfiguration:DbConfiguration
    {
        public TeamConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            DbInterception.Add(new TeamInterceptorTransientErrors());
            DbInterception.Add(new TeamInterceptorLogging());
        }
    }
}