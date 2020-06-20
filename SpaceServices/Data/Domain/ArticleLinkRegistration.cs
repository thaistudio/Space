using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data.Domain
{
    public class ArticleLinkRegistration
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ArticleId { get; set; }
        public string LinkId { get; set; }
    }
}
