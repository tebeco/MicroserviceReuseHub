using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Shared
{
    public static class AsyncStream
    {
        public static async IAsyncEnumerable<int> Generatestream(int factor)
        {
            for (var i = 0; i < 5; i++)
            {
                yield return i * factor;
                await Task.Delay(500);
            }
        }

        public static async Task EnumerateStream(IAsyncEnumerable<int> stream, ILogger logger)
        {
            await foreach (var item in stream)
            {
                logger.LogInformation("Received back: {item}", item);
            }
        }

        public static IAsyncEnumerable<int> EnumerateBackStream(IAsyncEnumerable<int> stream, ILogger logger) =>
            EnumerateBackStream(stream, 10, logger);

        public static async IAsyncEnumerable<int> EnumerateBackStream(IAsyncEnumerable<int> stream, int factor, ILogger logger)
        {
            await foreach (var item in stream)
            {
                logger.LogInformation("Received back: {item}", item);
                yield return item * factor;
            }
        }
    }
}
