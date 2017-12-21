using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Driver.Core;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace CDO.Models
{
    public class UserRepository
    {
        private IMongoClient          _client;
        private IMongoDatabase        _database;
        private IMongoCollection<Bug> _collection;

        public UserRepository()
        {
            _client     = new MongoClient("mongodb://127.0.0.1:27017");
            _database   = _client.GetDatabase("cdo");
            _collection = _database.GetCollection<Bug>("users");
        }
        
        public void GetUser(string email)
        {
            var query  = _collection.Find(Builders<BsonDocument>.Filter.Eq("Email", email).ToJson());
            Console.WriteLine(query);
        }
    }
}