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

        public NotificationData(ISqlDataAccess db, IHubContext<NotificationHub> context)
        {
            _context = context;

            this._db = db;

            _dependency = new SqlTableDependency<Notification>("Data Source=.;Initial Catalog=HamMgmtDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", "Notification");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        public Task<IEnumerable<NotificationModel>> GetEvents()
        {
            string sql = "select * from dbo.Notification";

            return _db.LoadData<NotificationModel, dynamic>(sql, new { });
        }

        private async void Changed(object sender, RecordChangedEventArgs<Notification> e)
        {
            var notifications = await GetEvents();
            await _context.Clients.All.SendAsync("ReceiveNotification", notifications.ToList());
        }
    }
}
