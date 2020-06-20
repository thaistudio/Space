using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Domain
{
    public class ArticleLinkRegistrations
    {
        public ArticleLinkRegistrations()
        {
            Registrations = new List<ArticleLinkRegistration>();
        }
        
        public List<ArticleLinkRegistration> Registrations { get; set; }
    }
}
