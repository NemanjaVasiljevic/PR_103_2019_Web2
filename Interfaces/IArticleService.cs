using PR_103_2019.Dtos;

namespace PR_103_2019.Interfaces
{
    public interface IArticleService
    {
        List<ArticleDto> GetAllArticle();
        ArticleDto GetArticle(long id);
        bool AddArticle(ArticleDto article, long sellerId);
        bool UpdateArticle(ArticleDto article, long sellerId);
        bool DeleteArticle(long articleId);

    }
}
