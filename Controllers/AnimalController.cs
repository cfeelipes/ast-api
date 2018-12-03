using System.Threading.Tasks;
using ast_api.Models;
using ast_api.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ast_api.Controllers
{
    [Produces("application/json")]
    [Route("api/Animal")]
    [EnableCors("MyPolicy")]
    public class AnimalController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }
        
        // GET: api/Animal
        [HttpGet]
        public async Task<IActionResult> ListarAnimais()
        {
            return new ObjectResult(await _animalRepository.ListarAnimais());
        }

        // GET: api/Animal/5c02ae24b7bfc11b24458801
        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarAnimal(string id)
        {
            var animal = await _animalRepository.RecuperarAnimal(id);
            
            if (animal == null)
                return new NotFoundResult();
            
            return new ObjectResult(animal);
        }

        // POST: api/Animal
        [HttpPost]
        public async Task<IActionResult> CriarAnimal([FromBody]Animal animal)
        {
            if (animal == null)
                return new NotFoundResult();

            await _animalRepository.CriarAnimal(animal);
            return new OkObjectResult(animal);
        }

        // PUT: api/Animal/5c02ae24b7bfc11b24458801
        [HttpPut("{name}")]
        public async Task<IActionResult> AlterarAnimal(string id, [FromBody]Animal animal)
        {
            var animalFromDb = await _animalRepository.RecuperarAnimal(id);
            
            if (animalFromDb == null)
                return new NotFoundResult();

            animal.Id = animalFromDb.Id;
            await _animalRepository.AlterarAnimal(animal);
            return new OkObjectResult(animal);
        }

        // DELETE: api/Animal/5c02ae24b7bfc11b24458801
        [HttpDelete("{nome}")]
        public async Task<IActionResult> DeletarAnimal(string id)
        {
            var animalFromDb = await _animalRepository.RecuperarAnimal(id);

            if (animalFromDb == null)
                return new NotFoundResult();

            await _animalRepository.DeletarAnimal(id);
            return new OkResult();
        }
    }
}