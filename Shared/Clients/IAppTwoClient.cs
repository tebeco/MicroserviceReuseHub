using System.Threading.Tasks;

namespace Shared.Clients
{
    public interface IAppTwoClient : IAppClient
    {
        Task DoBarAsync();
    }
}
