using System.Threading.Tasks;
using ast_api.Models;
using ast_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ast_api.Controllers
{
    [Produces("application/json")]
    [Route("api/Animal")]
    public class AnimalController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }
        
        // GET: api/Animal
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await _animalRepository.ListarAnimais());
        }

        // GET: api/Animal/nome
        [HttpGet("{name}", Name = "Get")]
        public async Task<IActionResult> Get(string nome)
        {
            var animal = await _animalRepository.RecuperarAnimal(nome);
            
            if (animal == null)
                return new NotFoundResult();
            
            return new ObjectResult(animal);
        }

        // POST: api/Animal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Animal animal)
        {
            if (animal == null)
                return new NotFoundResult();

            await _animalRepository.InserirAnimal(animal);
            return new OkObjectResult(animal);
        }

        // PUT: api/Animal/5
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string nome, [FromBody]Animal animal)
        {
            var animalFromDb = await _animalRepository.RecuperarAnimal(nome);
            
            if (animalFromDb == null)
                return new NotFoundResult();

            animal.Id = animalFromDb.Id;
            await _animalRepository.AlterarAnimal(animal);
            return new OkObjectResult(animal);
        }

        // DELETE: api/Animal/5
        [HttpDelete("{nome}")]
        public async Task<IActionResult> DeletarAnimal(string nome)
        {
            var animalFromDb = await _animalRepository.RecuperarAnimal(nome);

            if (animalFromDb == null)
                return new NotFoundResult();

            await _animalRepository.DeletarAnimal(nome);
            return new OkResult();
        }
    }
}