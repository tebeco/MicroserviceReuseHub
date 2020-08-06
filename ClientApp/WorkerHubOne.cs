using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Clients;

namespace ClientApp
{
    public class WorkerHubOne : BackgroundService
    {
        private readonly ILogger<WorkerHubOne> _logger;
        private readonly IAppOneClient _appOneClient;

        public WorkerHubOne(ILogger<WorkerHubOne> logger, IAppOneClient appOneClient)
        {
            _logger = logger;
            _appOneClient = appOneClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            await _appOneClient.OnConnectedAsync();

            _appOneClient.HubConnection.On("Foo", () =>
            {
                _logger.LogInformation("On Foo");
            });

            _appOneClient.HubConnection.On("DoingFoo", () =>
            {
                _logger.LogInformation("The hub is doing Foo");
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Calling DoFoo");
                var receivedStream = _appOneClient.DoFooAsync(GenerateDataStream());
                await EnumerateStream(receivedStream);
                await Task.Delay(4000);
            }
        }

        public async IAsyncEnumerable<int> GenerateDataStream()
        {
            for (var i = 0; i < 5; i++)
            {
                yield return i;
                await Task.Delay(500);
            }
            //After the for loop has completed and the local function exits the stream completion will be sent.
        }

        private async Task EnumerateStream(IAsyncEnumerable<int> receivedStream)
        {
            await foreach (var i in receivedStream)
            {
                _logger.LogInformation("Received back: {item}", i);
            }
        }
    }
}
