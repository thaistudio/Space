using AutoMapper;
using SpaceServices.Data.Domain;
using SpaceServices.Data.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceServices.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly ArticleStore articleStore;
        private readonly IMapper mapper;

        public ArticleService(ArticleStore articleStore,
                            IMapper mapper)
        {
            this.articleStore = articleStore;
            this.mapper = mapper;
        }

        public async Task<ArticleResponse> CreateArticle(Article article)
        {
            var created = await articleStore.CreateAsync(article);
            var articleResponse = mapper.Map<Article, ArticleResponse>(created);

            return articleResponse;
        }

        public async Task<ArticleResponse> GetArticle(string id)
        {
            var article = await articleStore.GetAsync(id);

            if (article is null)
                return null;

            var articleResponse = mapper.Map<Article, ArticleResponse>(article);

            return articleResponse;
        }

        public async Task<ArticlesResponse> GetArticles(Pagination pagination = null) 
        {
            var articles = await articleStore.GetAsync(pagination);
            var articlesResponse = new ArticlesResponse();

            foreach (var article in articles)
            {
                var articleResponse = mapper.Map<Article, ArticleResponse>(article);
                articlesResponse.Articles.Add(articleResponse);
            }

            return articlesResponse;
        }

        public async Task<int> GetArticlesPageCount(Pagination pagination)
        {
            var articlesCount = await articleStore.CountAsync();
            var pageCount = (int)Math.Ceiling(decimal.Divide(articlesCount, pagination.PageSize));

            return pageCount;
        }
    }
}
