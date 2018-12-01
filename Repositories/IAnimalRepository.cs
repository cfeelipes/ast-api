using System.Collections.Generic;
using System.Threading.Tasks;
using ast_api.Models;

namespace ast_api.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> ListarAnimais();
        Task<Animal> RecuperarAnimal(string name);
        Task InserirAnimal(Animal animal);
        Task<bool> AlterarAnimal(Animal animal);
        Task<bool> DeletarAnimal(string name);
    }
}