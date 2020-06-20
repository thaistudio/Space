using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Request
{
    public class ArticleLinkRegistrationRequest : BaseRequest
    {
        public string ArticleId { get; set; }
        public string LinkId { get; set; }
    }
}
