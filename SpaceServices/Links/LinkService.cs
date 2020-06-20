using AutoMapper;
using SpaceServices.Data.Domain;
using SpaceServices.Data.Request;
using SpaceServices.Data.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceServices.Links
{
    public class LinkService : ILinkService
    {
        private readonly LinkStore linkStore;
        private readonly IMapper mapper;

        public LinkService(LinkStore linkStore,
                            IMapper mapper)
        {
            this.linkStore = linkStore;
            this.mapper = mapper;
        }

        public async Task<LinkResponse> CreateLink(Link link)
        {
            var createdLink = await linkStore.CreateAsync(link);
            var linkResponse = mapper.Map<Link, LinkResponse>(createdLink);
            return linkResponse;
        }

        public async Task<LinksResponse> GetLinks()
        {
            var links = await linkStore.GetAsync();

            var linksResponse = new LinksResponse();
            foreach (var link in links)
            {
                var linkResponse = mapper.Map<Link, LinkResponse>(link);
                linksResponse.Links.Add(linkResponse);
            }

            return linksResponse;
        }
    }
}
