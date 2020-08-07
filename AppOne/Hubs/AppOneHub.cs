using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Clients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppOne.Hubs
{
    public class AppOneHub : Hub
    {
        private readonly IAppTwoClient _appTwoClient;
        private readonly ILogger<AppOneHub> _logger;

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

        public async Task<IAsyncEnumerable<int>> DoFoo(IAsyncEnumerable<int> stream)
        {
            _logger.LogInformation("Doing 'Foo'");
            await Clients.All.SendAsync("DoingFoo");
            return _appTwoClient.StreamKixAsync(stream);

        }
    }
}
