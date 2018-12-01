using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ast_api.Models
{
    public class Animal
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Nome { get; set; }        
        public string Sexo { get; set; }

        // [BsonElement("ElementName")]
        // public string Teste { get; set; }
    }
}