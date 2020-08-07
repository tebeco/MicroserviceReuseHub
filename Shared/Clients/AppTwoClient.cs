using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Shared.Clients
{
    public class AppTwoClient : AppClientBase, IAppTwoClient
    {
        public AppTwoClient(IConfiguration configuration, IHostApplicationLifetime hostApplicationLifetime)
            : base(new Uri(configuration.GetServiceUri("apptwo"), "/AppTwoHub"), hostApplicationLifetime)
        { }

        public IAsyncEnumerable<int> StreamDuplexTwo(IAsyncEnumerable<int> stream)
        {
            return HubConnection.StreamAsync<int>("DuplexTwo", stream);
        }

        public async Task<ChannelReader<int>> StreamDuplexTwoChannel(ChannelReader<int> channel)
        {
            return await HubConnection.StreamAsChannelAsync<int>("DuplexTwoChannel", channel);
        }
    }
}
