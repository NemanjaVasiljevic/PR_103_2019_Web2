using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PR_103_2019.Data;
using PR_103_2019.Dtos;
using PR_103_2019.Interfaces;
using PR_103_2019.Models;
using PR_103_2019.Services;

namespace PR_103_2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly PR_103_2019Context _context;
        private readonly IArticleService _articleService;

        public ArticlesController(PR_103_2019Context context, IArticleService articleService)
        {
            _context = context;
            _articleService = articleService;
        }

        // GET: api/Articles
        [HttpGet]
        public IActionResult GetArticle()
        {
            return Ok(_articleService.GetAllArticle());
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public IActionResult GetArticle(long articleId)
        {
            return Ok(_articleService.GetArticle(articleId));
        }

        [HttpGet("seller/{id}")]
        public IActionResult GetArticleBySellerId(long sellerId)
        {
            return Ok(_articleService.GetAllArticlesBySellerId(sellerId));
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult UpdateArticle(long id, ArticleDto article)
        {
            try
            {
                _articleService.UpdateArticle(article, id);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult CreateArticle(ArticleDto article, long sellerId)
        {
            try
            {
                _articleService.AddArticle(article, sellerId);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteArticle(long articleId)
        {
            try
            {
                _articleService.DeleteArticle(articleId);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        private bool ArticleExists(long id)
        {
            return (_context.Article?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
