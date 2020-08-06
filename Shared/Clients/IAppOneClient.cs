using System.Threading.Tasks;

namespace Shared.Clients
{
    public interface IAppOneClient : IAppClient
    {
        Task DoFooAsync();
    }
}
