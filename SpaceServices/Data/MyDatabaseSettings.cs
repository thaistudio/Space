using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data
{
    public class MyDatabaseSettings : IMyDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string LinksCollectionName { get; set; }
        public string ArticlesCollectionName { get; set; }
        public string ArticleLinkRegistrationsCollectionName { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMyDatabaseSettings
    {
        string LinksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ArticlesCollectionName { get; set; }
        string ArticleLinkRegistrationsCollectionName { get; set; }
    }
}
