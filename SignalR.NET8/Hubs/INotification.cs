namespace SignalR.NET8.Hubs
{
    public interface INotification
    {
        Task Notify(string message);
        Task NotifyDict(List<KeyValuePair<string, string>> dict);
    }
}
