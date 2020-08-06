using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Shared.Clients
{
    public class AppTwoClient : AppClientBase, IAppTwoClient
    {
        public AppTwoClient(IConfiguration configuration, IHostApplicationLifetime hostApplicationLifetime)
            : base(
                  new Uri(
                      configuration.GetServiceUri("apptwo") ?? null,// new Uri("https://localhost:5003/"),
                      "/AppTwoHub"
                      ),
                  hostApplicationLifetime)
        { }

        public async Task DoBarAsync()
        {
            await HubConnection.SendAsync("DoBar");
        }
    }
}
