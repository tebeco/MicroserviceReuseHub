using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Clients;

namespace ClientApp
{
    public class WorkerHubTwo : BackgroundService
    {
        private readonly ILogger<WorkerHubTwo> _logger;
        private readonly IAppTwoClient _appTwoClient;

        public WorkerHubTwo(ILogger<WorkerHubTwo> logger, IAppTwoClient appTwoClient)
        {
            _logger = logger;
            _appTwoClient = appTwoClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            await _appTwoClient.OnConnectedAsync();

            _appTwoClient.HubConnection.On("Bar", () =>
            {
                _logger.LogInformation("On Bar");
            });

            _appTwoClient.HubConnection.On("DoingBar", () =>
            {
                _logger.LogInformation("The hub is doing Bar");
            });
        }
    }
}
