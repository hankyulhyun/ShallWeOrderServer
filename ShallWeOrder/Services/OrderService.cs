using System.Threading.Tasks;
using Grpc.Core;

namespace ShallWeOrder
{
    public class OrderService : Order.OrderBase
    {
        public OrderService()
        {

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