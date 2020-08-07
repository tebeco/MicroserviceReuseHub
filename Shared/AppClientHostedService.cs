using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Shared
{
    public class AppClientHostedService<TClient> : BackgroundService
        where TClient : IAppClient
    {
        private readonly TClient _appClient;

        public AppClientHostedService(TClient appClient)
        {
            _appClient = appClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _appClient.StartAsync();
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            await _appClient.StopAsync();
        }
    }
}