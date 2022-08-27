using System.Threading.Tasks;
using BrokerRequests;
using MassTransit;

namespace ClientService.Consumers;

public class GetExampleConsumer : IConsumer<GetExampleRequest>
{
    public Task Consume(ConsumeContext<GetExampleRequest> context)
    {
        return context.RespondAsync<GetExampleResponse>(new GetExampleResponse { IsSuccess = true });
    }
}