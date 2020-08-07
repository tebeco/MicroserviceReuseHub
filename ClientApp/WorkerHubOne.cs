using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared;
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
            //make sure this does not block startup as it forces the continuation to be scheduled on ANY OTHER threadpool worker this SynchronizationContext is null => returns imediatly
            await Task.Yield();

            //Waiting for connection to be done
            await _appOneClient.OnConnectedAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                var stream = _appOneClient.StartDuplexOneAsync(AsyncStream.Generatestream(1));
                await AsyncStream.EnumerateStream(stream, _logger);

                await Task.Delay(4000);
            }
        }
    }
}
