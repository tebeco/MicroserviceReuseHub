using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Shared.Clients
{
    public interface IAppTwoClient : IAppClient
    {
        IAsyncEnumerable<int> StreamDuplexTwo(IAsyncEnumerable<int> stream);

        Task<ChannelReader<int>> StreamDuplexTwoChannel(ChannelReader<int> stream);
    }
}
