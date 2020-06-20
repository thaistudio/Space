using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceServices.ArticleLinkRegistrations;
using SpaceServices.Articles;
using SpaceServices.Data.Domain;
using SpaceServices.Data.Request;
using SpaceServices.Data.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaiSpaceApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/articles")]
    [ApiController]
    public class ArticlesV1Controller : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IArticleLinkRegistrationService articleLinkRegistrationService;
        private readonly IMapper mapper;

        public ArticlesV1Controller(IArticleService articleService,
                                    IArticleLinkRegistrationService articleLinkRegistrationService,
                                    IMapper mapper)
        {
            this.articleService = articleService;
            this.articleLinkRegistrationService = articleLinkRegistrationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<ArticlesResponse>>> Get([FromQuery]Pagination pagination = null)
        {
            var articles = await articleService.GetArticles(pagination);

            var currentBaseUri = $"{Request.Scheme}://{Request.Host.Value}";
            var pageCount = await articleService.GetArticlesPageCount(pagination);

            var pagedResponse = new PagedResponse<ArticlesResponse>
            {
                Data = articles,
                PageNumber = pagination != null ? pagination.PageNumber : 0,
                PageSize = pagination != null ? pagination.PageSize : 0,
                NextPage = pagination.PageNumber >= pageCount ? pageCount : pagination.PageNumber + 1,
                PreviousPage = pagination.PageNumber >= pageCount ? pageCount - 1 : pagination.PageNumber - 1,
                PageCount = pageCount
            };

            return Ok(pagedResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticlesResponse>> Get(string id)
        {
            var article = await articleService.GetArticle(id);

            if (article is null)
                return NotFound();

            return Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleResponse>> Create(ArticleRequest articleRequest)
        {
            var article = mapper.Map<ArticleRequest, Article>(articleRequest);

            var created = await articleService.CreateArticle(article);

            if (created is null)
                return NoContent();

            return Created($"{Request.Path}/{created.Id}", created);
        }

        [HttpGet("{id}/links")]
        public async Task<ActionResult<LinksResponse>> GetLinks(string id)
        {
            var linksResponse = await articleLinkRegistrationService.GetLinksByArticle(id);

            if (linksResponse.Links.Count == 0)
                return NoContent();

            return Ok(linksResponse);
        }
    }
}
