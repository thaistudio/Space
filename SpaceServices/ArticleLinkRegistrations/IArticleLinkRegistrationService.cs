using SpaceServices.Data.Domain;
using SpaceServices.Data.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceServices.ArticleLinkRegistrations
{
    public interface IArticleLinkRegistrationService
    {
        public Task<Data.Domain.ArticleLinkRegistrations> UpdateRegistrations(Data.Domain.ArticleLinkRegistrations registrations);
        public Task<LinksResponse> GetLinksByArticle(string articleId);
    }
}
