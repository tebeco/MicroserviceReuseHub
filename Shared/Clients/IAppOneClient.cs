using System.Collections.Generic;

namespace Shared.Clients
{
    public interface IAppOneClient : IAppClient
    {
        IAsyncEnumerable<int> StartDuplexOneAsync(IAsyncEnumerable<int> stream);
    }
}
