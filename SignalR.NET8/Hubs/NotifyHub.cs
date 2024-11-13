using Microsoft.AspNetCore.SignalR;
using SignalR.NET8.Hubs;
using System.Collections.Concurrent;

namespace SignalR.NET8.Hubs;

public class NotifyHub : Hub<INotification>
{
    public static readonly ConcurrentDictionary<string, string> groups = new();



    public override Task OnConnectedAsync()
    {
        groups.TryAdd(Context.ConnectionId, "test1");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        groups.TryRemove(Context.ConnectionId, out _);
        return base.OnDisconnectedAsync(exception);
    }


    public async Task InvokeGroup()
    {
        groups.TryGetValue(Context.ConnectionId, out var groupVal);
        //await Clients.All.SendAsync("TEST GROUP", CancellationToken.None);
        await Clients.Caller.Notify(Context.ConnectionId);
    }

    //public async Task GetAllItemsAsync()
    //{
    //    groups.TryAdd("one", "john");
    //    groups.TryAdd("two", "doe");
    //    groups.TryAdd("three", "unkown");

    //    await Clients.All.SendAsync(groups.ToList());
    //}
}
