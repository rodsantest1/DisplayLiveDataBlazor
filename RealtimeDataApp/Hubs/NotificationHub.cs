using Microsoft.AspNetCore.SignalR;
using RealtimeDataApp.Data;

namespace RealtimeDataApp.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task EventNotification(List<NotificationModel> notifications)
        {
            await Clients.All.SendAsync("ReceiveNotification", notifications);
        }
    }
}
