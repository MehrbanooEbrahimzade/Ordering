using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MassTransit;

namespace Email.Api.Producer
{
    public class OperationFinishedProducer
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public OperationFinishedProducer(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task SendOperationFinished(OperationFinishedCommand command)
        {
            
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(EventBusConstants.OperationFinishedQueue));

            await endpoint.Send(new OperationFinishedEvent { UserName = command.UserName, Number = command.Number });
        }
    }
}
