using System;
using ast_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ast_api.Contexts
{
    public class AnimalContext : IAnimalContext
    {
        private readonly IMongoDatabase _db;

        public AnimalContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Animal> Animais => _db.GetCollection<Animal>("Animais");
    }
}