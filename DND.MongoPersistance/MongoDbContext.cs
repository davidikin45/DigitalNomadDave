using MongoDB.Bson;
using MongoDB.Driver;
using Solution.Base.Implementation.Persistance.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.MongoPersistance
{
    public class MongoApplicationDbContext : BaseMongoDBContext
    {
        public IMongoCollection<user> Users
        {
            get
            {
                return Database.GetCollection<user>("user");
            }
        }

    }

    public class user
    {
        public string name;
    }
}
