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
        private readonly IAppTwoClient _appTwoClient;

        public WorkerHubOne(ILogger<WorkerHubOne> logger, IAppOneClient appOneClient, IAppTwoClient appTwoClient)
        {
            _logger = logger;
            _appOneClient = appOneClient;
            _appTwoClient = appTwoClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            await _appOneClient.OnConnectedAsync();
            await _appTwoClient.OnConnectedAsync();

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
                await _appOneClient.DoFooAsync(generateDataStream());
                await Task.Delay(4000);
            }

            async IAsyncEnumerable<int> generateDataStream()
            {
                for (var i = 0; i < 5; i++)
                {
                    yield return i;
                    await Task.Delay(500);
                }
                //After the for loop has completed and the local function exits the stream completion will be sent.
            }
        }
    }
}
