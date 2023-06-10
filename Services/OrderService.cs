using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Exceptions;
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
            TimeZoneInfo belgradeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

            orderDb.OrdredDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgradeTimeZone);
            Random rnd = new Random();
            orderDb.ArrivalDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMinutes(rnd.Next(1, 60)), belgradeTimeZone);



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

        public void DeleteOrder(long orderId)
        {
            Order order = dbContext.Order.Find(orderId);
            if(order != null)
            {
                Article article = dbContext.Article.Find(order.ArticleId);

                if (article == null)
                {
                    throw new ResourceNotFoundException("Article with specified id doesn't exist!");
                }

                article.Quantity += order.ArticleQuantity;
                dbContext.Order.Remove(order);
                dbContext.SaveChanges();
            }
        }

        public List<OrderDto> GetAllOrders()
        {
            List<OrderDto> res = mapper.Map<List<OrderDto>>(dbContext.Order.ToList());
            foreach (var item in res)
            {
                item.BuyerName = dbContext.User.FirstOrDefault(u => u.Id == item.BuyerId).Name;
                item.ArticleName = dbContext.Article.FirstOrDefault(a => a.Id == item.ArticleId).Name;
            }

            return res;
        }

        public OrderDto GetOrderById(long orderId)
        {
            return mapper.Map<OrderDto>(dbContext.Order.Find(orderId));
        }

        public OrderDto UpdateOrder(OrderDto orderDto, long articleId)
        {
            Order orderDb = mapper.Map<Order>(orderDto);
            Article article = dbContext.Article.FirstOrDefault(a => a.OrderId == orderDto.Id);

            if(article != null)
            {
                //ovaj artikal vec postoji u porudzbini
                if (article.Quantity > orderDb.ArticleQuantity)
                {
                    article.Quantity -= orderDb.ArticleQuantity;
                    orderDb.Status = OrderState.RESERVED;
                    orderDb.TotalPrice = article.Price * orderDb.ArticleQuantity;
                    orderDb.OrdredDate = DateTime.UtcNow;
                    Random rnd = new Random();
                    orderDb.ArrivalDate = DateTime.UtcNow.AddMinutes(rnd.Next(60, 240));
                }
                dbContext.SaveChanges();
            }
            else
            {
                //dodaje novi artikal u ponudu
                Article newArticle = dbContext.Article.FirstOrDefault(a => a.Id == articleId);
                if (newArticle != null)
                {
                    if (newArticle.Quantity > orderDb.ArticleQuantity)
                    {
                        newArticle.Quantity -= orderDb.ArticleQuantity;
                        orderDb.Status = OrderState.RESERVED;
                        orderDb.TotalPrice = newArticle.Price * orderDb.ArticleQuantity;
                        orderDb.OrdredDate = DateTime.UtcNow;
                        Random rnd = new Random();
                        orderDb.ArrivalDate = DateTime.UtcNow.AddMinutes(rnd.Next(60, 240));
                    }
                    dbContext.SaveChanges();
                }
            }

            return mapper.Map<OrderDto>(orderDb);
        }
    }
}
