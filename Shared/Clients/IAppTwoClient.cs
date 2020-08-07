using System.Collections.Generic;

namespace Shared.Clients
{
    public interface IAppTwoClient : IAppClient
    {
        IAsyncEnumerable<int> StreamDuplexTwo(IAsyncEnumerable<int> stream);
    }
}
