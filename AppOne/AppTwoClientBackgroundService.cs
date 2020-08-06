using Microsoft.Extensions.Hosting;
using Shared.Clients;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace AppOne
{
    public class AppTwoClientBackgroundService : BackgroundService
    {
        private readonly IAppTwoClient _appTwoClient;
        private readonly ILogger<AppTwoClientBackgroundService> _logger;

        public AppTwoClientBackgroundService(IAppTwoClient appTwoClient, ILogger<AppTwoClientBackgroundService> logger)
        {
            _appTwoClient = appTwoClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _appTwoClient.OnConnectedAsync();

            _appTwoClient.HubConnection.On("Bar", () =>
            {
                _logger.LogInformation("On Bar");
            });
        }
    }
}
