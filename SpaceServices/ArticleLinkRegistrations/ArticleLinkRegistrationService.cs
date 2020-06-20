using AutoMapper;
using SpaceServices.Articles;
using SpaceServices.Data.Domain;
using SpaceServices.Data.Response;
using SpaceServices.Links;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceServices.ArticleLinkRegistrations
{
    public class ArticleLinkRegistrationService : IArticleLinkRegistrationService
    {
        private readonly ArticleLinkRegistrationStore articleLinkRegistrationStore;
        private readonly LinkStore linkStore;
        private readonly IMapper mapper;

        public ArticleLinkRegistrationService(ArticleLinkRegistrationStore articleLinkRegistrationStore,
                                            LinkStore linkStore,
                                            IMapper mapper)
        {
            this.articleLinkRegistrationStore = articleLinkRegistrationStore;
            this.linkStore = linkStore;
            this.mapper = mapper;
        }

        public async Task<LinksResponse> GetLinksByArticle(string articleId)
        {
            var linkIds = await articleLinkRegistrationStore.GetLinkIdsByArticleAsync(articleId);
            var links = await linkStore.GetAsync(linkIds);

            var linksResponse = new LinksResponse();
            foreach (var link in links)
            {
                var linkResponse = mapper.Map<Link, LinkResponse>(link);
                linksResponse.Links.Add(linkResponse);
            }
            return linksResponse;
        }

        public async Task<Data.Domain.ArticleLinkRegistrations> UpdateRegistrations(Data.Domain.ArticleLinkRegistrations registrations)
        {
            await articleLinkRegistrationStore.UpdateMultipleAsync(registrations);

            return registrations;
        }
    }
}
