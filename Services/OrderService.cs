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
                throw new ResourceNotFoundException("User with this id does not exist");
            }
            else
            {
                orderDb.Buyer = buyer;
            }

            Article article = dbContext.Article.Find(orderDto.ArticleId);
            if(article == null)
            {
                throw new ResourceNotFoundException("Article with this id does not exist");
            }

            orderDb.SellerId = article.SellerId;
            orderDb.SellerName = dbContext.User.Find(article.SellerId).Name;
            article.Quantity -= orderDb.ArticleQuantity;
            orderDb.Status = OrderState.RESERVED;
            orderDb.TotalPrice = article.Price * orderDb.ArticleQuantity;
            TimeZoneInfo belgradeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

            orderDb.OrdredDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgradeTimeZone);
            Random rnd = new Random();
            orderDb.ArrivalDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddHours(rnd.Next(1, 48)), belgradeTimeZone);



            try
            {
                dbContext.Add(orderDb);
                dbContext.SaveChanges();

                return mapper.Map<OrderDto>(orderDb);
            }
            catch (Exception)
            {

                throw new ForbiddenActionException();
            }

        }

        public void DeleteOrder(long orderId)
        {
            Order order = dbContext.Order.Find(orderId);
            if(order != null)
            {

                //Vreme kada se moze otkazati porudzbina
                DateTime minAllowedCancelDate = order.OrdredDate.AddHours(1);

                if (DateTime.UtcNow >= minAllowedCancelDate)
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
                else
                {
                    throw new ForbiddenActionException("Order cannot be deleted as it has not reached the minimum allowed order date.");
                }
            }
        }

        public List<OrderDto> GetAllOrders()
        {
            List<OrderDto> res = mapper.Map<List<OrderDto>>(dbContext.Order.ToList());

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

                    TimeZoneInfo belgradeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

                    orderDb.OrdredDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgradeTimeZone);
                    Random rnd = new Random();
                    orderDb.ArrivalDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddHours(rnd.Next(1, 48)), belgradeTimeZone);
                }
                dbContext.SaveChanges();
            }
            else
            {
                //dodaje novi artikal u ponudu
                Article newArticle = dbContext.Article.FirstOrDefault(a => a.Id == articleId);
                if (newArticle != null)
                {
                    orderDb.SellerId = newArticle.SellerId;
                    orderDb.ArticleId = newArticle.Id;
                    if (newArticle.Quantity > orderDb.ArticleQuantity)
                    {
                        newArticle.Quantity -= orderDb.ArticleQuantity;
                        orderDb.Status = OrderState.RESERVED;
                        orderDb.TotalPrice = newArticle.Price * orderDb.ArticleQuantity;

                        TimeZoneInfo belgradeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

                        orderDb.OrdredDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgradeTimeZone);
                        Random rnd = new Random();
                        orderDb.ArrivalDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddHours(rnd.Next(1, 48)), belgradeTimeZone);
                    }
                    dbContext.SaveChanges();
                }
            }

            return mapper.Map<OrderDto>(orderDb);
        }
    }
}
