using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shared.Clients
{
    public interface IAppOneClient : IAppClient
    {
        Task DoFooAsync(IAsyncEnumerable<int> dataStream);
    }
}
