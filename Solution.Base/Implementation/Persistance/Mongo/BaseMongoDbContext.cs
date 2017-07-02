using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Implementation.Persistance.Mongo
{
    public abstract class BaseMongoDBContext
    {
        public IMongoDatabase Database;

        public BaseMongoDBContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultMongoConnectionString"].ConnectionString;
            var databaseName = ConfigurationManager.AppSettings["DefaultMongoDatabaseName"];
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(databaseName);
        }

        public BsonDocument BuildInfo()
        {
            var buildInfoCommand = new BsonDocument("buildinfo", 1);
            BsonDocument result = Database.RunCommand<BsonDocument>(buildInfoCommand);
            return result;
        }
    }
}
