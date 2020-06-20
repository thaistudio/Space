using AutoMapper;
using SpaceServices.Data.Domain;
using SpaceServices.Data.Request;
using SpaceServices.Data.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Link, LinkResponse>();
            CreateMap<LinkRequest, Link>();
            CreateMap<Article, ArticleResponse>();
            CreateMap<ArticleRequest, Article>();
            CreateMap<ArticleLinkRegistrationRequest, ArticleLinkRegistration>();
        }
    }
}
