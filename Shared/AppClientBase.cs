using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;

namespace Shared
{
    public abstract class AppClientBase : IAppClient
    {
        private readonly Uri _hubUri;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly TaskCompletionSource<int> _hubConnectionConnected = new TaskCompletionSource<int>();

        public AppClientBase(Uri hubUri, IHostApplicationLifetime hostApplicationLifetime)
        {
            _hubUri = hubUri;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public HubConnection HubConnection { get; private set; }

        public async Task StartAsync()
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUri)
                .WithAutomaticReconnect()
                .Build();

            try
            {
                await HubConnection.StartAsync();

                if (_hostApplicationLifetime.ApplicationStopping.IsCancellationRequested || _hostApplicationLifetime.ApplicationStopped.IsCancellationRequested)
                    _hubConnectionConnected.SetCanceled();
                else
                    _hubConnectionConnected.SetResult(0);
            }
            catch (Exception ex)
            {
                _hubConnectionConnected.SetException(ex);
            }
        }

        public Task<int> OnConnectedAsync() => _hubConnectionConnected.Task;

        public Task StopAsync() => HubConnection.StopAsync();
    }
}
