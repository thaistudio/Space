using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceServices.Data;
using SpaceServices.Data.Domain;
using SpaceServices.Data.Request;
using SpaceServices.Data.Response;
using SpaceServices.Links;

namespace ThaiSpaceApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/links")]
    [ApiController]
    public class LinksV1Controller : ControllerBase
    {
        private readonly ILinkService linksService;
        private readonly IMapper mapper;
        private readonly IBackgroundJobClient backgroundJob;

        public LinksV1Controller(ILinkService linksService,
                                IMapper mapper,
                                IBackgroundJobClient backgroundJob)
        {
            this.linksService = linksService;
            this.mapper = mapper;
            this.backgroundJob = backgroundJob;
        }

        [HttpGet]
        public async Task<ActionResult<LinksResponse>> Get() => Ok(await linksService.GetLinks());

        //[HttpGet("{id:length(24)}", Name = "GetLink")]
        //public ActionResult<Link> Get(string id)
        //{
        //    var link = linksService.GetAsync(id);

        //    if (link == null)
        //        return NotFound();

        //    return link;
        //}

        [HttpPost]
        public async Task<ActionResult<LinkResponse>> Create(LinkRequest linkRequest)
        {
            var link = mapper.Map<LinkRequest, Link>(linkRequest);
            var created = await linksService.CreateLink(link);
            
            if (created is null)
                return NoContent();
            return Created($"{Request.Path}/{created.Id}", created);
        }
    }
}