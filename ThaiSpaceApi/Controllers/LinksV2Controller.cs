using Microsoft.AspNetCore.Mvc;
using SpaceServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThaiSpaceApi.Services;

namespace ThaiSpaceApi.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/links")]
    [ApiController]
    public class LinksV2Controller : ControllerBase
    {
        //private readonly LinksService linksService;

        //public LinksV2Controller(LinksService linksService)
        //{
        //    this.linksService = linksService;
        //}

        //[HttpGet]
        //public ActionResult<List<Link>> Get()
        //{
        //    var links = new List<Link>
        //    {
        //        new Link { Url = "v2" },
        //        new Link { Url = "v2 new"}
        //    };

        //    return links;
        //}

        //[HttpGet("{id:length(24)}", Name = "GetLink")]
        //public ActionResult<Link> Get(string id)
        //{
        //    var link = linksService.Get(id);

        //    if (link == null)
        //        return NotFound();

        //    return link;
        //}

        //[HttpPost]
        //public ActionResult<Link> Create(Link link)
        //{
        //    linksService.Create(link);
        //    return Ok();
        //}
    }
}
