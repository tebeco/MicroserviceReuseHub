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

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(4000);
            }
        }

            }
        }
    }
}
