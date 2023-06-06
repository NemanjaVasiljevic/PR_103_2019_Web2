using AutoMapper;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Interfaces;
using PR_103_2019.Models;

namespace PR_103_2019.Services
{
    public class OrderService : IOrderService
    {
        PR_103_2019Context dbContext;
        IMapper mapper;
        public OrderService(PR_103_2019Context db, IMapper map)
        {
            dbContext = db;
            mapper = map;
        }

        public OrderDto CreateOrder(OrderDto orderDto, long userId)
        {
            Order orderDb = mapper.Map<Order>(orderDto);
            User buyer = dbContext.User.Find(userId);

            if(buyer == null)
            {
                return null;
            }
            else
            {
                orderDb.Buyer = buyer;
            }

            Article article = dbContext.Article.Find(orderDto.ArticleId);
            if(article == null)
            {
                return null;
            }

            article.Quantity -= orderDb.ArticleQuantity;
            orderDb.Status = OrderState.RESERVED;
            orderDb.TotalPrice = article.Price * orderDb.ArticleQuantity;
            orderDb.OrdredDate = DateTime.UtcNow;
            Random rnd = new Random();
            orderDb.ArrivalDate = DateTime.UtcNow.AddMinutes(rnd.Next(1,60));

            

            try
            {
                dbContext.Add(orderDb);
                dbContext.SaveChanges();

                return mapper.Map<OrderDto>(orderDb);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public void DeleteOrder(long orderId, long userId)
        {
            Order order = dbContext.Order.Find(orderId);
            if(order != null)
            {
                dbContext.Order.Remove(order);
                dbContext.SaveChanges();
            }
        }

        public List<OrderDto> GetAllOrders()
        {
            return mapper.Map<List<OrderDto>>(dbContext.Order.ToList());
        }

        public OrderDto GetOrderById(long orderId)
        {
            return mapper.Map<OrderDto>(dbContext.Order.Find(orderId));
        }
    }
}
