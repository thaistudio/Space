using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Response
{
    public class ArticleLinkRegistrationsResponse
    {
        public ArticleLinkRegistrationsResponse()
        {
            Registrations = new List<ArticleLinkRegistrationReponse>();
        }
        public List<ArticleLinkRegistrationReponse> Registrations { get; set; }
    }
}
