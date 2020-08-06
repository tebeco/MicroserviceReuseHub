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

        public IAsyncEnumerable<int> DoFooAsync(IAsyncEnumerable<int> dataStream)
        {
            return HubConnection.StreamAsync<int>("DoFoo", dataStream);
        }
    }
}
