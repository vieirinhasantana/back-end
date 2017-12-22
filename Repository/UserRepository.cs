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
        private IMongoCollection<User> _collection;

        public UserRepository()
        {
            _client     = new MongoClient("mongodb://127.0.0.1:27017");
            _database   = _client.GetDatabase("cdo");
            _collection = _database.GetCollection<User>("users");
        }
        
        public List<User> GetUser(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            var query = _collection.Find(filter).ToList();
            return query;
        }
    }
}