using System.Collections.Generic;
using System.Threading.Tasks;
using ast_api.Contexts;
using ast_api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ast_api.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly IAnimalContext _context;
        
        public AnimalRepository(IAnimalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Animal>> ListarAnimais() 
        {
            return await _context
                            .Animais
                            .Find(_ => true)
                            .ToListAsync();
        }

        public Task<Animal> RecuperarAnimal(string nome) 
        {
            FilterDefinition<Animal> filter = Builders<Animal>.Filter.Eq(m => m.Nome, nome);
            return _context
                .Animais
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task InserirAnimal(Animal animal)
        {
            await _context.Animais.InsertOneAsync(animal);
        }

        public async Task<bool> AlterarAnimal(Animal animal)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Animais
                        .ReplaceOneAsync(
                            filter: g => g.Id == animal.Id,
                            replacement: animal);
            
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeletarAnimal(string nome)
        {
            FilterDefinition<Animal> filter = Builders<Animal>.Filter.Eq(m => m.Nome, nome);
            DeleteResult deleteResult = await _context
                                                .Animais
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}