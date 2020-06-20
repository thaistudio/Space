using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Response
{
    public class ArticlesResponse
    {
        public ArticlesResponse()
        {
            Articles = new List<ArticleResponse>();
        }
        public List<ArticleResponse> Articles { get; set; }
    }
}
