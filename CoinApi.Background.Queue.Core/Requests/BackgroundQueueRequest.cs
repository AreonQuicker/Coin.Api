using CoinApi.Background.Queue.Core.Enums;

namespace CoinApi.Background.Queue.Core.Requests
{
    public class BackgroundQueueRequest
    {
        public BackgroundQueueRequest(BackgroundQueueRequestTypeEnum type, params string[] keys)
        {
            Type = type;
            Keys = keys;
        }

        public BackgroundQueueRequestTypeEnum Type { get; }
        public string[] Keys { get; }
    }
}