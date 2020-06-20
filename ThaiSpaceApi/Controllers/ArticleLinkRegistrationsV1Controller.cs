using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceServices.ArticleLinkRegistrations;
using SpaceServices.Data.Domain;
using SpaceServices.Data.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaiSpaceApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/articleLinkRegistrations")]
    [ApiController]
    public class ArticleLinkRegistrationsV1Controller : ControllerBase
    {
        private readonly IArticleLinkRegistrationService articleLinkRegistrationService;
        private readonly IMapper mapper;

        public ArticleLinkRegistrationsV1Controller(IArticleLinkRegistrationService articleLinkRegistrationService,
                                                    IMapper mapper)
        {
            this.articleLinkRegistrationService = articleLinkRegistrationService;
            this.mapper = mapper;
        }

        [HttpPost("update")]
        public async Task<ActionResult<ArticleLinkRegistrations>> Update(ArticleLinkRegistrationsRequest registrationsRequest)
        {
            var registrations = new ArticleLinkRegistrations();
            foreach (var linkId in registrationsRequest.LinkIds)
            {
                var registration = new ArticleLinkRegistration
                {
                    ArticleId = registrationsRequest.ArticleId,
                    LinkId = linkId
                };
                registrations.Registrations.Add(registration);
            }

            var updated = await articleLinkRegistrationService.UpdateRegistrations(registrations);

            return Ok(updated);
        }
    }
}
