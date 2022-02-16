using System.Threading;
using System.Threading.Tasks;
using CoinApi.Background.Queue.Core.Requests;

namespace CoinApi.Background.Queue.Core.Interfaces
{
    /// <summary>
    /// Will be used to queue up background requests
    /// </summary>
    public interface IBackgroundQueue
    {
        /// <summary>
        /// Queue up a new background request
        /// </summary>
        /// <param name="backgroundQueueRequest">Request object</param>
        /// <returns></returns>
        ValueTask QueueAsync(BackgroundQueueRequest backgroundQueueRequest);

        /// <summary>
        /// Dequeue request from channel one by one
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<BackgroundQueueRequest> DequeueAsync(
            CancellationToken cancellationToken);
    }
}