using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Shared
{
    public static class AsyncStream
    {
        public static async IAsyncEnumerable<int> GenerateStream(int factor)
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

        public static ChannelReader<int> GenerateChannelReader(int factor)
        {
            var channel = Channel.CreateUnbounded<int>();

            _ = Task.Run(async () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    await channel.Writer.WriteAsync(i * factor);
                    await Task.Delay(500);
                }
                channel.Writer.Complete();
            });

            return channel.Reader;
        }

        public static Task EnumerateChannel(ChannelReader<int> source, ILogger logger) =>
            EnumerateChannel(source, logger, null, null);

        public static Task EnumerateChannel(ChannelReader<int> source, ILogger logger, Func<int, Task> perItemAction, Func<Task> postAction)
        {
            perItemAction ??= _ => Task.CompletedTask;
            postAction ??= () => Task.CompletedTask;

            return Task.Run(async () =>
            {
                while (await source.WaitToReadAsync())
                {
                    while (source.TryRead(out var item))
                    {
                        logger.LogInformation("Receveid: {item}", item);
                        await perItemAction.Invoke(item);
                    }
                }

                await postAction.Invoke();
            });
        }

        public static Task EnumerateBackChannel(ChannelReader<int> source, ChannelWriter<int> destination, ILogger logger) =>
            EnumerateBackChannel(source, destination, 10, logger);

        public static Task EnumerateBackChannel(ChannelReader<int> source, ChannelWriter<int> destination, int factor, ILogger logger) =>
            EnumerateChannel(
                source,
                logger,
                async (item) =>
                {
                    logger.LogInformation("Sending : {item}", item * factor);
                    await destination.WriteAsync(item * factor);
                },
                () =>
                {
                    destination.Complete();
                    return Task.CompletedTask;
                });
    }
}
