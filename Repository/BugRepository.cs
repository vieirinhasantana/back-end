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
        }

        public List<Bug> GetOne(string objectId)
        {
            return _collection.Find(Builders<Bug>.Filter.Eq("IdBug", objectId)).ToList();
        }
        public string InsertOne(string title, string severity, string description, string email, string status, string image)
        {

            //Guid g;
            var document = new Bug
            {
                IdBug = Guid.NewGuid().ToString(),
                Title = title,
                Severity = severity,
                Description = description,
                Email = email,
                Timestamp = DateTime.Now,
                Status = status,
                Image = image
            };
            _collection.InsertOne(document);
            return "successful";
        }

        public string Update(string objectId)
        {
            _collection.FindOneAndUpdate(Builders<Bug>.Filter.Eq("IdBug", objectId), Builders<Bug>.Update.Set("Status", "fechado"));
            return "successful";
        }
    }
}