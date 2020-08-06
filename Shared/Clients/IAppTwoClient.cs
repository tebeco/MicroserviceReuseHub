using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Clients
{
    public interface IAppTwoClient : IAppClient
    {
        Task DoBarAsync();

        IAsyncEnumerable<int> StreamKixAsync(IAsyncEnumerable<int> dataStream);
    }
}
