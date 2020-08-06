using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Clients
{
    public interface IAppTwoClient : IAppClient
    {
        Task DoBarAsync();

        Task StreamKixAsync(IAsyncEnumerable<int> dataStream);
    }
}
