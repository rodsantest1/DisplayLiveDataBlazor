using Microsoft.AspNetCore.SignalR;
using RealtimeDataApp.Hubs;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;

namespace RealtimeDataApp.Data
{
    public class NotificationData
    {
        private readonly ISqlDataAccess _db;
        private readonly IHubContext<NotificationHub> _context;

        private readonly SqlTableDependency<Notification> _dependency;

        public NotificationData(ISqlDataAccess db)
        {
            this._db = db;
        }

        public Task<IEnumerable<NotificationModel>> GetEvents()
        {
            string sql = "select * from dbo.Notification";

            return _db.LoadData<NotificationModel, dynamic>(sql, new { });
        }
    }
}
