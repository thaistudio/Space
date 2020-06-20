using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaiSpaceApi.Models
{
    public class ApiResponse<T>
    {
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Successful { get; set; }
        public string Token { get; set; }
    }
}
