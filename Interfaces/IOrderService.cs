using PR_103_2019.Dtos;

namespace PR_103_2019.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders();
        OrderDto GetOrderById(long orderId);
        OrderDto CreateOrder(OrderDto orderDto, long userId);
        void DeleteOrder(long orderId, long userId);
    }
}
