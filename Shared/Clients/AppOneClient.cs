using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Shared.Clients
{
    public class AppOneClient : AppClientBase, IAppOneClient
    {
        public AppOneClient(IConfiguration configuration, IHostApplicationLifetime hostApplicationLifetime)
            : base(new Uri(configuration.GetServiceUri("appone"), "/AppOneHub"), hostApplicationLifetime)
        { }

        public async Task DoFooAsync()
        {
            await HubConnection.SendAsync("DoFoo");
        }

        public async Task StartKixAsync(HubConnection hubConnection)
        {
            await hubConnection.SendAsync("Kix", clientStreamData());

            async IAsyncEnumerable<int> clientStreamData()
            {
                for (var i = 0; i < 5; i++)
                {
                    yield return i;
                    await Task.Delay(1000);
                }
                //After the for loop has completed and the local function exits the stream completion will be sent.
            }
        }
    }
}
