using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Request
{
    public class LinkRequest : BaseRequest
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
