using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Mongo
{
    public class MongoSetting : IMongoSetting
    {
        public string ConnectionString { get; private set; } = "mongodb://localhost:27017/";
        public string CollectionName { get; private set; } = "firemessages";
        public string Database { get; private set; } = "irongate";
    }
}
