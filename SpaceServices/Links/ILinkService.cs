using SpaceServices.Data.Domain;
using SpaceServices.Data.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceServices.Links
{
    public interface ILinkService
    {
        public Task<LinksResponse> GetLinks();
        public Task<LinkResponse> CreateLink(Link link);
    }
}
