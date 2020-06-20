using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Response
{
    public class LinkResponse : BaseReponse
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
