using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shared.Clients
{
    public class AppOneClient : AppClientBase, IAppOneClient
    {
        private readonly ILogger<AppOneClient> _logger;

        public AppOneClient(IConfiguration configuration, IHostApplicationLifetime hostApplicationLifetime, ILogger<AppOneClient> logger)
            : base(new Uri(configuration.GetServiceUri("appone"), "/AppOneHub"), hostApplicationLifetime)
        {
            _logger = logger;
        }

        public IAsyncEnumerable<int> StartDuplexOneAsync(IAsyncEnumerable<int> stream)
        {
            _logger.LogInformation("Calling DuplexOne");

            return HubConnection.StreamAsync<int>("DuplexOne", stream);
        }

        public async Task<ChannelReader<int>> StreamDuplexOneChannelAsync(ChannelReader<int> channel)
        {
            return await HubConnection.StreamAsChannelAsync<int>("DuplexOneChannel", channel);
        }
    }
}
