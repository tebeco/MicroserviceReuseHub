using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppTwo.Hubs
{
    public class AppTwoHub : Hub
    {
        private readonly ILogger<AppTwoHub> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public AppTwoHub(ILogger<AppTwoHub> logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
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

        public async Task DoBar()
        {
            _logger.LogInformation("Doing 'Bar'");
            await Clients.All.SendAsync("Bar");
        }

        public IAsyncEnumerable<int> Kix(IAsyncEnumerable<int> stream)
        {
            return EnumerateStreamBack(stream);
        }

        private async IAsyncEnumerable<int> EnumerateStreamBack(IAsyncEnumerable<int> stream)
        {
            await foreach (var item in stream)
            {
                _logger.LogInformation("kix : {i}", item);
                yield return item;
            }
        }
    }
}
