using Microsoft.AspNetCore.SignalR;
using RealtimeDataApp.Data;
using RealtimeDataApp.Hubs;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;

namespace RealtimeDataApp.Services
{
    public class NotificationService
    {
        private readonly NotificationData _db;
        private readonly IHubContext<NotificationHub> _context;
        private readonly SqlTableDependency<Notification> _dependency;
        private readonly string _connectionString;

        public NotificationService(NotificationData db, IHubContext<NotificationHub> context)
        {
            _context = context;
            _db = db;
            _connectionString = "Server=.;Database=HamMgmtDB;Trusted_Connection=True;";
            _dependency = new SqlTableDependency<Notification>(_connectionString, "Notification");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Notification> e)
        {
            var notifications = await GetAllNotifications();
            await _context.Clients.All.SendAsync("ReceiveNotification", notifications);
        }

        public Task<IEnumerable<NotificationModel>> GetAllNotifications()
        {
            return _db.GetEvents();
        }

    }
}
