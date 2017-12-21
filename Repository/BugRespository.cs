using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Bson;

namespace CDO.Models
{
    public class BugRepository
    {
        private IMongoClient          _client;
        private IMongoDatabase        _database;
        private IMongoCollection<Bug> _collection;

        public BugRepository()
        {
            _client     = new MongoClient("mongodb://127.0.0.1:27017");
            _database   = _client.GetDatabase("cdo");
            _collection = _database.GetCollection<Bug>("bugs");
        }

        public List<Bug> GetAll()
        {
            return _collection.Find(new BsonDocument()).ToList();
            //return "dsasdddsads";
        }
    }
}