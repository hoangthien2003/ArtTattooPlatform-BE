using Microsoft.AspNetCore.SignalR;

namespace back_end.Hubs
{
    public class MessageHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage(string methodName, object data)
        {
            await Clients.All.SendAsync(methodName, data);
        }

        public string GetConnectionId() => Context.ConnectionId;

        public async Task SendBookingToManager(string user, string receiverConnectionId, object data)
        {
            await Clients.Client(receiverConnectionId).SendAsync("BookingService", user, data);
        }
    }
}
