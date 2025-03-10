using Microsoft.AspNetCore.Mvc;
using ActorLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActorsREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private ActorsRepository _actorsRepository;

        public ActorsController(ActorsRepository actorsRepository)
        {
            _actorsRepository = actorsRepository;
        }

        // GET: api/<ActorsController>
        [HttpGet]
        public IEnumerable<Actor> GetActors()
        {
            return _actorsRepository.GetActors();
        }

        // GET api/<ActorsController>/5
        [HttpGet("{id}")]
        public Actor? GetById(int id)
        {
            return _actorsRepository.GetById(id);
        }

        // POST api/<ActorsController>
        [HttpPost]
        public Actor Post([FromBody] Actor newActor)
        {
            return _actorsRepository.Add(newActor);
        }

        // PUT api/<ActorsController>/5
        [HttpPut("{id}")]
        public Actor? Put(int id, [FromBody] Actor updates)
        {
            return _actorsRepository.Update(id, updates);
        }

        // DELETE api/<ActorsController>/5
        [HttpDelete("{id}")]
        public Actor? Delete(int id)
        {
            return _actorsRepository.Delete(id);
        }
    }
}
