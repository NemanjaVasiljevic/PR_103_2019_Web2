using PR_103_2019.Dtos;
using PR_103_2019.Models;

namespace PR_103_2019.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders();
        OrderDto GetOrderById(long orderId);
        OrderDto CreateOrder(OrderDto orderDto, long userId);
        OrderDto UpdateOrder(OrderDto orderDto, long articleId);
        void DeleteOrder(long orderId, long userId);
    }
}
