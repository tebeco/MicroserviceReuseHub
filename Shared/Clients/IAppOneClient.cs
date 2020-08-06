using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Shared.Clients
{
    public interface IAppOneClient : IAppClient
    {
        Task DoFooAsync();

        Task StartKixAsync(HubConnection hubConnection);
    }
}
