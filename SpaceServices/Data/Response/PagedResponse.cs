using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Response
{
    public class PagedResponse<T>
    {
        public T Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public int PageCount { get; set; }
    }
}
