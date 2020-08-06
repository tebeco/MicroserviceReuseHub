﻿using Microsoft.AspNetCore.SignalR;
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
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public AppOneHub(IAppTwoClient appTwoClient, ILogger<AppOneHub> logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            _appTwoClient = appTwoClient;
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("+++++++++++++ CLIENT JOIN");
            await _appTwoClient.DoBarAsync();
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

            await _appTwoClient.StartAsync();
            return _appTwoClient.StreamKixAsync(stream);

            //async IAsyncEnumerable<int> internalIterate(IAsyncEnumerable<int> stream)
            //{
            //    await foreach (var i in stream)
            //    {
            //        yield return i;
            //    }
            //}
        }
    }
}
