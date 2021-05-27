using EasyNetQ;
using MECS.Core.Data.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading.Tasks;

namespace MECS.MessageBus.Bus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;
        private IAdvancedBus _advancedBus;
        private readonly string _stringConnection;
        public MessageBus(string stringConnection)
        {
            _stringConnection = stringConnection;
            TryConnect();
        }

        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;
        public IAdvancedBus AdvancedBus => _bus?.Advanced;
        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            _bus.PubSub.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await _bus.PubSub.PublishAsync(message);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return await _bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }



        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.Subscribe(subscriptionId, onMessage);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }
        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
           where TRequest : IntegrationEvent
           where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Respond(responder);
        }

        public async Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return await _bus.Rpc.RespondAsync(responder);
        }

        private void TryConnect()
        {
            if (IsConnected)
                return;

            var polly = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt =>
                     TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            polly.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_stringConnection);
                _advancedBus = _bus.Advanced;
                _advancedBus.Disconnected += OnDisconnect;
            });
        }
        private void OnDisconnect(object s, EventArgs e)
        {

            var polly = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();
            polly.Execute(TryConnect);
        }
        public void Dispose()
        {
            _bus?.Dispose();
        }
    }
}
