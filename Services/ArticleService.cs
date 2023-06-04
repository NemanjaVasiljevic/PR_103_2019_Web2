using AutoMapper;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Interfaces;
using PR_103_2019.Models;

namespace PR_103_2019.Services
{
    public class ArticleService : IArticleService
    {
        private PR_103_2019Context dbContext;
        private IMapper mapper;

        public ArticleService(PR_103_2019Context db, IMapper map)
        {
            dbContext = db;
            mapper = map;
        }

        public bool AddArticle(ArticleDto article, long userId)
        {
            Article articleDb = mapper.Map<Article>(article);
            articleDb.Seller = dbContext.User.Find(userId);
            articleDb.Orders = new List<Order>();

            dbContext.Add(articleDb);
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteArticle(long articleId)
        {
            Article articleDb = dbContext.Article.Find(articleId);
            if (articleDb != null)
            {
                try
                {
                    dbContext.Remove(articleDb);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false ;
                }
            }
            else
            {
                return false;
            }
        }

        public List<ArticleDto> GetAllArticle()
        {
            return mapper.Map<List<ArticleDto>>(dbContext.Article.ToList());
        }

        public ArticleDto GetArticle(long id)
        {
            return mapper.Map<ArticleDto>(dbContext.Article.Find(id));
        }

        public bool UpdateArticle(ArticleDto article, long sellerId)
        {
            Article articleDb = dbContext.Article.Find(article.Id);

            if(articleDb != null)
            {
                articleDb.Name = article.Name;
                articleDb.Description = article.Description;
                articleDb.Seller = dbContext.User.Find(sellerId);
                if (articleDb.Seller == null)
                {
                    return false;
                }
                articleDb.Price = article.Price;
                articleDb.Quantity = article.Quantity;

                dbContext.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
