using Microsoft.AspNetCore.Mvc;
using ActorLib;
using System.Collections.Generic;

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // GET: api/<ActorsController>
        [HttpGet]
        public ActionResult<IEnumerable<Actor>> GetActors()
        {
            IEnumerable<Actor> result = _actorsRepository.GetActors();
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET api/<ActorsController>/5
        [HttpGet("{id}")]
        public ActionResult<Actor> GetById(int id)
        {
            Actor? actor = _actorsRepository.GetById(id);
            if (actor != null)
            {
                return Ok(actor);
            }
            return NotFound();
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
