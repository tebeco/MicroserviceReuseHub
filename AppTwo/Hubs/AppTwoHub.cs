using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AppTwo.Hubs
{
    public class AppTwoHub : Hub
    {
        private readonly ILogger<AppTwoHub> _logger;

        public AppTwoHub(ILogger<AppTwoHub> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("+++++++++++++ CLIENT JOIN");
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("+++++++++++++ CLIENT LEFT");
            return Task.CompletedTask;
        }

        /*/
        public IAsyncEnumerable<int> DuplexTwo()
        {
            return AsyncStream.Generatestream(100);
        }
        /*/
        public IAsyncEnumerable<int> DuplexTwo(IAsyncEnumerable<int> incommingStream)
        {
            return AsyncStream.EnumerateBackStream(incommingStream, _logger);
        }
        //*/

        public ChannelReader<int> DuplexTwoChannel(ChannelReader<int> requestChannel)
        {
            var responseChannel = Channel.CreateUnbounded<int>();
            AsyncStream.EnumerateBackChannel(requestChannel, responseChannel.Writer, _logger);

            return responseChannel.Reader;
        }
    }
}
