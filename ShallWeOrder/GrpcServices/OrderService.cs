using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace ShallWeOrder.GrpcService
{
    public class OrderService : Orderer.OrdererBase
    {
        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;
        }

        public override Task<CreateOrderReply> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CreateOrderReply
            {
                Result = 200,
            });
        }
    }
}