using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Shared
{
    public interface IAppClient
    {
        HubConnection HubConnection { get; }

        Task<int> OnConnectedAsync();

        Task StartAsync();

        Task StopAsync();
    }
}
