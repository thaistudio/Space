using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Response
{
    public class ArticleLinkRegistrationReponse : BaseReponse
    {
        public string Id { get; set; }
        public string ArticleId { get; set; }
        public string LinkId { get; set; }
    }
}
