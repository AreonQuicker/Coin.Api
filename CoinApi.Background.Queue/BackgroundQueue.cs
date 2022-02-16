using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using CoinApi.Background.Queue.Core.Interfaces;
using CoinApi.Background.Queue.Core.Requests;

namespace CoinApi.Background.Queue
{
    /// <summary>
    /// Bounded channel will be use for the queue
    /// </summary>
    public class BackgroundQueue : IBackgroundQueue
    {
        private readonly Channel<BackgroundQueueRequest> _queue;

        public BackgroundQueue(int capacity)
        {
            var options = new BoundedChannelOptions(capacity) {FullMode = BoundedChannelFullMode.Wait};
            _queue = Channel.CreateBounded<BackgroundQueueRequest>(options);
        }

        public async ValueTask QueueAsync(BackgroundQueueRequest backgroundQueueRequest)
        {
            if (backgroundQueueRequest is null) throw new ArgumentNullException(nameof(backgroundQueueRequest));

            await _queue.Writer.WriteAsync(backgroundQueueRequest);
        }

        public async ValueTask<BackgroundQueueRequest> DequeueAsync(
            CancellationToken cancellationToken)
        {
            var workItem =
                await _queue.Reader.ReadAsync(cancellationToken);

            return workItem;
        }
    }
}