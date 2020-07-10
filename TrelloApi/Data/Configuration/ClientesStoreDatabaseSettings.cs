using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloApi.Data.Configuration
{
    public class ClientesStoreDatabaseSettings : IClientesStoreDatabaseSettings
    {
       // public MongoClient Client { get; }
       
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
