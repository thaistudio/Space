using SpaceServices.Data.Domain;
using SpaceServices.Data.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceServices.Articles
{
    public interface IArticleService
    {
        public Task<ArticlesResponse> GetArticles(Pagination pagination);
        public Task<ArticleResponse> CreateArticle(Article article);
        public Task<ArticleResponse> GetArticle(string id);
        public Task<int> GetArticlesPageCount(Pagination pagination);
    }
}
