using ast_api.Models;
using MongoDB.Driver;

namespace ast_api.Contexts
{
    public interface IAnimalContext
    {
        IMongoCollection<Animal> Animais { get; }
    }
}