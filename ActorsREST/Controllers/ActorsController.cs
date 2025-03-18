using Microsoft.AspNetCore.Mvc;
using ActorLib;
using System.Collections.Generic;
using ActorsREST.Records;
using Microsoft.AspNetCore.Cors;

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
        public ActionResult<IEnumerable<Actor>> GetActors(
            [FromQuery] string? inputName,
            [FromQuery] int? inputBirthYearBefore,
            [FromQuery] int? inputBirthYearAfter,
            [FromQuery] string? inputSortBy)
        {
            IEnumerable<Actor> result = _actorsRepository.GetActors(
                name:inputName,
                birthYearBefore:inputBirthYearBefore,
                birthYearAfter:inputBirthYearAfter,
                sortby:inputSortBy);
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

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST api/<ActorsController>
        [HttpPost]
        public ActionResult<Actor> Post([FromBody] ActorRecord newActorRecord)
        {
            try
            {
                Actor converted = RecordHelper.ConvertActorRecord(newActorRecord);
                Actor createdActor = _actorsRepository.Add(converted);
                return Created("/" + createdActor.Id, createdActor);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Dit objekt indeholder nulls!: " + ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest("Dit objekt er ude af en range!: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Dit objekt er ikke gyldigt!: " + ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT api/<ActorsController>/5
        [HttpPut("{id}")]
        public ActionResult<Actor> Put(int id, [FromBody] ActorRecord updates)
        {
            try
            {
                Actor converted = RecordHelper.ConvertActorRecord(updates);
                Actor? updated = _actorsRepository.Update(id, converted);
                if (updated != null)
                {
                    return Ok(updated);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Dit objekt indeholder nulls!: " + ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest("Dit objekt er ude af en range!: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Dit objekt er ikke gyldigt!: " + ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE api/<ActorsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Actor> Delete(int id)
        {
            Actor? deleted = _actorsRepository.Delete(id);
            if (deleted != null)
            {
                return Ok(deleted);
            }
            return NotFound();
        }
    }
}
