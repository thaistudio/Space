using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Response
{
    public class LinksResponse : BaseReponse
    {
        public LinksResponse()
        {
            Links = new List<LinkResponse>();
        }
        public List<LinkResponse> Links { get; set; }
    }
}
