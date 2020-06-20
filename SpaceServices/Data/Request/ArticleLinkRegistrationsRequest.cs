using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Request
{
    public class ArticleLinkRegistrationsRequest
    {
        public string ArticleId { get; set; }
        public List<string> LinkIds { get; set; }
    }
}
