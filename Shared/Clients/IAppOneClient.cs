using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Shared.Clients
{
    public interface IAppOneClient : IAppClient
    {
        IAsyncEnumerable<int> StartDuplexOneAsync(IAsyncEnumerable<int> stream);

        Task<ChannelReader<int>> StreamDuplexOneChannelAsync(ChannelReader<int> channel);

    }
}
