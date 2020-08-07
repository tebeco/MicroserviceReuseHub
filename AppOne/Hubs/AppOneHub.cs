using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Clients;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AppOne.Hubs
{
    public class AppOneHub : Hub
    {
        private readonly ILogger<AppOneHub> _logger;
        private readonly IAppTwoClient _appTwoClient;

        public AppOneHub(IAppTwoClient appTwoClient, ILogger<AppOneHub> logger)
        {
            _appTwoClient = appTwoClient;
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("+++++++++++++ CLIENT JOIN");
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("+++++++++++++CLIENT LEFT");
            return Task.CompletedTask;
        }

        public IAsyncEnumerable<int> DuplexOne(IAsyncEnumerable<int> stream)
        {
            _logger.LogInformation("Doing 'DuplexOne'");

            return AsyncStream.EnumerateBackStream(_appTwoClient.StreamDuplexTwo(stream), _logger);
        }

        public async Task<ChannelReader<int>> DuplexOneChannel(ChannelReader<int> requestChannel)
        {
            var responseChannel = Channel.CreateUnbounded<int>();
            _ = AsyncStream.EnumerateBackChannel(requestChannel, responseChannel.Writer, _logger);

            return await _appTwoClient.StreamDuplexTwoChannel(responseChannel.Reader);
        }
    }
}
