﻿using Irongate.Element.Actors.TheTrooper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Mongo
{
    public class MongoRepository : IMongoRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private IMongoSetting _mongoSettings;

        public MongoRepository(IMongoSetting mongoSettings)
        {
            _mongoSettings = mongoSettings;
            _mongoClient = new MongoClient(_mongoSettings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_mongoSettings.Database);
        }

        public async Task<bool> SaveSomething(object fireStuff, string collection)
        {
            var col = _mongoDatabase.GetCollection<object>(collection);
            await col.InsertOneAsync(fireStuff);
            return true;
        }
    }
}
